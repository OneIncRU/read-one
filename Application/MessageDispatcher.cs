using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReadOne.Application
{
    /// <summary>
    /// This implements a basic message dispatcher, driving the overall command handling
    /// and event application/distribution process. It is suitable for a simple, single
    /// node application that can safely build its subscriber list at startup and keep
    /// it in memory. Depends on some kind of event storage mechanism.
    /// </summary>
    public class MessageDispatcher
    {
        /// <summary>
        /// Tries to send the specified command to its handler. Throws an exception
        /// if there is no handler registered for the command.
        /// </summary>
        public void SendCommand(ICommand c)
        {
            var t = c.GetType();
            if (_handlers.ContainsKey(t)) _handlers[t](c);
            else throw new Exception($"No command handler registered for {t}!");
        }

        /// <summary>
        /// Registers an aggregate as being the handler for a particular
        /// command.
        /// </summary>
        public void AddHandlerFor<C, A>() where A : Aggregate, new() where C : ICommand
        {
            var t = typeof(C);
            if (_handlers.ContainsKey(t)) throw new Exception($"Command handler already registered for {t}!");

            _handlers.Add(typeof(C), c =>
            {
                // Create an empty aggregate.
                var agg = new A();

                // Load the aggregate with events.
                agg.Id = ((dynamic)c).Id;
                agg.ApplyEvents(_eventStore.LoadEventsFor<A>(agg.Id));

                // With everything set up, we invoke the command handler, collecting the
                // events that it produces.
                var resultEvents = new ArrayList();
                foreach (var e in (agg as IHandleCommand<C>).Handle((C)c))
                    resultEvents.Add(e);

                // Store the events in the event store.
                if (resultEvents.Count > 0)
                    _eventStore.SaveEventsFor<A>(agg.Id, agg.EventsLoaded, resultEvents);

                // Publish them to all subscribers.
                foreach (var e in resultEvents) PublishEvent(e);
            });
        }

        /// <summary>
        /// Adds an object that subscribes to the specified event, by virtue of implementing
        /// the ISubscribeTo interface.
        /// </summary>
        public void AddSubscriberFor<E>(ISubscribeTo<E> subscriber) where E : IDomainEvent
        {
            if (!_subscribers.ContainsKey(typeof(E)))
                _subscribers.Add(typeof(E), new List<Action<object>>());
            _subscribers[typeof(E)].Add(e => subscriber.Handle((E)e));
        }

        /// <summary>
        /// Looks thorugh the specified assembly for all public types that implement
        /// the IHandleCommand or ISubscribeTo generic interfaces. Registers each of
        /// the implementations as a command handler or event subscriber.
        /// </summary>
        public void ScanAssembly(Assembly assembly)
        {
            // Scan for and register handlers.
            var handlers =
                from t in assembly.GetTypes()
                from i in t.GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(IHandleCommand<>)
                let args = i.GetGenericArguments()
                select new
                {
                    CommandType = args[0],
                    AggregateType = t
                };
            foreach (var h in handlers)
                GetType().GetMethod("AddHandlerFor")
                    .MakeGenericMethod(h.CommandType, h.AggregateType)
                    .Invoke(this, new object[] { });

            // Scan for and register subscribers.
            var subscriber =
                from t in assembly.GetTypes()
                from i in t.GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(ISubscribeTo<>)
                select new
                {
                    Type = t,
                    EventType = i.GetGenericArguments()[0]
                };
            foreach (var s in subscriber)
                GetType().GetMethod("AddSubscriberFor")
                    .MakeGenericMethod(s.EventType)
                    .Invoke(this, new object[] { CreateInstanceOf(s.Type) });
        }

        /// <summary>
        /// Looks at the specified object instance, examples what commands it handles
        /// or events it subscribes to, and registers it as a receiver/subscriber.
        /// </summary>
        public void ScanInstance(object instance)
        {
            // Scan for and register handlers.
            var handlers =
                from i in instance.GetType().GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(IHandleCommand<>)
                let args = i.GetGenericArguments()
                select new
                {
                    CommandType = args[0],
                    AggregateType = instance.GetType()
                };
            foreach (var h in handlers)
                GetType().GetMethod("AddHandlerFor")
                    .MakeGenericMethod(h.CommandType, h.AggregateType)
                    .Invoke(this, new object[] { });

            // Scan for and register subscribers.
            var subscriber =
                from i in instance.GetType().GetInterfaces()
                where i.IsGenericType
                where i.GetGenericTypeDefinition() == typeof(ISubscribeTo<>)
                select i.GetGenericArguments()[0];
            foreach (var s in subscriber)
                GetType().GetMethod("AddSubscriberFor")
                    .MakeGenericMethod(s)
                    .Invoke(this, new object[] { instance });
        }

        /// <summary>
        /// Initializes a message dispatcher, which will use the specified event store
        /// implementation.
        /// </summary>
        public MessageDispatcher(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        private Dictionary<Type, Action<object>> _handlers = new Dictionary<Type, Action<object>>();
        private Dictionary<Type, List<Action<object>>> _subscribers = new Dictionary<Type, List<Action<object>>>();
        private IEventStore _eventStore;

        /// <summary>
        /// Publishes the specified event to all of its subscribers.
        /// </summary>
        private void PublishEvent(object @event)
        {
            var eventType = @event.GetType();
            if (_subscribers.ContainsKey(eventType))
                foreach (var sub in _subscribers[eventType])
                    sub(@event);
        }

        /// <summary>
        /// Creates an instance of the specified type. If you are using some kind
        /// of DI container, and want to use it to create instances of the handler
        /// or subscriber, you can plug it in here.
        /// </summary>
        private object CreateInstanceOf(Type t)
        {
            return Activator.CreateInstance(t);
        }
    }
}