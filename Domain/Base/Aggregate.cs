using System;
using System.Collections;
using System.Reflection;

namespace ReadOne
{
    /// <summary>
    /// Aggregate base class, which factors out some common infrastructure that
    /// all aggregates have (ID and event application).
    /// </summary>
    public class Aggregate
    {
        /// <summary>
        /// The number of events loaded into this aggregate.
        /// </summary>
        public int EventsLoaded { get; private set; }

        /// <summary>
        /// The unique ID of the aggregate.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Enumerates the supplied events and applies them in order to the aggregate.
        /// </summary>
        public void ApplyEvents(IEnumerable events)
        {
            foreach (var e in events)
                typeof(Aggregate).GetTypeInfo().GetDeclaredMethod("ApplyOneEvent")
                    .MakeGenericMethod(e.GetType())
                    .Invoke(this, new object[] { e });
        }

        /// <summary>
        /// Applies a single event to the aggregate.
        /// </summary>
        public void ApplyOneEvent<E>(E @event) where E : IDomainEvent
        {
            var applier = this as IApplyEvent<E>;
            if (applier == null)
            {
                var m = $"Aggregate {GetType().Name} does not know how to apply event {@event.GetType().Name}";
                throw new InvalidOperationException(m);
            }
            applier.Apply(@event);
            EventsLoaded++;
        }
    }
}