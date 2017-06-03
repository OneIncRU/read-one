using System;
using System.Collections.Generic;

namespace ReadOne
{
    public class Library :
        ISubscribeTo<BookAdded>
    {
        public Book[] GetBooks(string[] tags)
        {
            return Array.Empty<Book>();
        }

        public string[] GetTags()
        {
            return Array.Empty<string>();
        }

        public Book GetBookInfo()
        {
            return new Book();
        }

        public void Handle(BookAdded e)
        {
            var book = new Book { Id = e.Id, Name = e.Name };
            _books.Add(book);
        }

        public class Book
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        private readonly List<Book> _books = new List<Book>();
    }
}