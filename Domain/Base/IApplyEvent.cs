namespace ReadOne
{
    /// <summary>
    /// Implemented by an aggregate once for each event type it can apply.
    /// </summary>
    public interface IApplyEvent<T> where T : IDomainEvent
    {
        void Apply(T e);
    }
}