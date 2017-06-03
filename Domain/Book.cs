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
    public class Start : ICommand
    {
        public Guid Id { get; set; }
        public Guid Reader { get; set; }
    }
    public class Finish : ICommand
    {
        public Guid Id { get; set; }
        public Guid Reader { get; set; }
    }
    public class Review : ICommand
    {
        public Guid Id { get; set; }
        public Guid Reader { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string[] Tags { get; set; }
    }
    public class Remove : ICommand
    {
        public Guid Id { get; set; }
    }

    public class BookAdded : IDomainEvent
    {
        public DateTime Moment { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class BookStartedBySomebody : IDomainEvent
    {
        public DateTime Moment { get; set; }
        public Guid Id { get; set; }
        public Guid Reader { get; set; }
    }
    public class BookRead : IDomainEvent
    {
        public DateTime Moment { get; set; }
        public Guid Id { get; set; }
        public Guid Reader { get; set; }
    }
    public class BookReviewed : IDomainEvent
    {
        public DateTime Moment { get; set; }
        public Guid Id { get; set; }
        public Guid Reader { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string[] Tags { get; set; }
    }
    public class BookRemoved : IDomainEvent
    {
        public DateTime Moment { get; set; }
        public Guid Id { get; set; }
    }

    public class BookAlreadyAdded : Exception { }
}