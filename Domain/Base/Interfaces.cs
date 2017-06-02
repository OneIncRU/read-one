namespace ReadOne
{
    public interface IDomainObject { }


    public interface IValueObject : IDomainObject { }

    public interface IEntity : IDomainObject { }


    public interface IMessage : IValueObject { }

    public interface IDomainEvent : IMessage { }

    public interface ICommand : IMessage { }
}