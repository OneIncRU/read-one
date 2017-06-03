using System;
using System.Collections;

namespace ReadOne
{
    public class Book : Aggregate,
        IHandleCommand<Add>,
        IHandleCommand<Start>,
        IHandleCommand<Finish>,
        IHandleCommand<Review>,
        IHandleCommand<Remove>,
        IApplyEvent<BookAdded>
    {
        public IEnumerable Handle(Add c)
        {
            if (Id != default(Guid)) throw new BookAlreadyAdded();

            yield return new BookAdded
            {
                Moment = DateTime.Now,
                Id = c.Id,
                Name = c.Name
            };
        }

        public IEnumerable Handle(Start c)
        {
            if (Id == default(Guid)) throw new BookDoesNotExist();

            yield return new BookStartedBySomebody
            {
                Moment = DateTime.Now,
                Id = Id,
                Reader = c.Reader
            };
        }

        public IEnumerable Handle(Finish c)
        {
            if (Id == default(Guid)) throw new BookDoesNotExist();

            yield return new BookRead
            {
                Moment = DateTime.Now,
                Id = Id,
                Reader = c.Reader
            };
        }

        public IEnumerable Handle(Review c)
        {
            if (Id == default(Guid)) throw new BookDoesNotExist();

            yield return new BookReviewed
            {
                Moment = DateTime.Now,
                Id = Id,
                Reader = c.Reader,
                Comment = c.Comment,
                Rating = c.Rating,
                Tags = c.Tags
            };
        }

        public IEnumerable Handle(Remove c)
        {
            if (Id == default(Guid)) throw new BookDoesNotExist();

            yield return new BookRemoved
            {
                Moment = DateTime.Now,
                Id = Id
            };
        }

        public void Apply(BookAdded e)
        {
            Id = e.Id;
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
        public string Reader { get; set; }
    }
    public class Finish : ICommand
    {
        public Guid Id { get; set; }
        public string Reader { get; set; }
    }
    public class Review : ICommand
    {
        public Guid Id { get; set; }
        public string Reader { get; set; }
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
        public string Reader { get; set; }
    }
    public class BookRead : IDomainEvent
    {
        public DateTime Moment { get; set; }
        public Guid Id { get; set; }
        public string Reader { get; set; }
    }
    public class BookReviewed : IDomainEvent
    {
        public DateTime Moment { get; set; }
        public Guid Id { get; set; }
        public string Reader { get; set; }
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
    public class BookDoesNotExist : Exception { }
}