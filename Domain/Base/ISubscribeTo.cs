namespace ReadOne
{
    /// <summary>
    /// Implemented by anything that wishes to subscribe to an event emitted by
    /// an aggregate and successfully stored.
    /// </summary>
    public interface ISubscribeTo<T> where T : IDomainEvent
    {
        void Handle(T e);
    }
}