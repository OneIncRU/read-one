using System;

namespace ReadOne
{
    public interface IDomainObject { }


    public interface IValueObject : IDomainObject { }

    public interface IEntity : IDomainObject { }


    public interface IMessage : IValueObject
    {
        Guid Id { get; set; }
    }

    public interface IDomainEvent : IMessage { }

    public interface ICommand : IMessage { }
}