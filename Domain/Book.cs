using System;
using System.Collections;

namespace ReadOne
{
    public class Book : Aggregate,
        IHandleCommand<Add>,
        IApplyEvent<BookAdded>
    {
        public string Name { get; private set; }

        public IEnumerable Handle(Add c)
        {
            if (EventsLoaded > 0) throw new BookAlreadyAdded();

            yield return new BookAdded
            {
                Id = c.Id,
                Name = c.Name
            };
        }

        public void Apply(BookAdded e)
        {
            Id = e.Id;
            Name = e.Name;
        }
    }

    public class Add : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class BookAdded : IDomainEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class BookAlreadyAdded : Exception { }
}