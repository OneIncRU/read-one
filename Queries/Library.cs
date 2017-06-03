using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadOne
{
    public class Library :
        ISubscribeTo<BookAdded>,
        ISubscribeTo<BookRemoved>,
        ISubscribeTo<BookReviewed>
    {
        public BookPreview[] Books(string[] tags)
        {
            return _bookPreviews.ToArray();
        }

        public string[] Tags()
        {
            return _tags.ToArray();
        }

        public Book BookInfo(Guid id)
        {
            return _books.SingleOrDefault(book => book.Id == id);
        }

        #region Builders

        public void Handle(BookAdded e)
        {
            var book = new BookPreview { Id = e.Id, Name = e.Name };
            _bookPreviews.Add(book);

            var info = new Book { Id = e.Id, Name = e.Name };
            _books.Add(info);

            _tagsMap.Add(e.Id, new List<string>());
        }

        public void Handle(BookRemoved e)
        {
            var book = _bookPreviews.Single(x => x.Id == e.Id);
            _bookPreviews.Remove(book);

            var info = _books.Single(x => x.Id == e.Id);
            _books.Remove(info);

            _tagsMap.Remove(e.Id);
        }

        public void Handle(BookReviewed e)
        {
            var info = _books.Single(x => x.Id == e.Id);
            info.Reviews.Add(new Review { Reader = e.Reader, Comment = e.Comment, Rating = e.Rating });
            foreach(var tag in e.Tags)
            {
                if (!info.Tags.Contains(tag)) info.Tags.Add(tag);
            }
            info.Rating = info.Reviews.Sum(r => r.Rating) / info.Reviews.Count;

            foreach(var tag in e.Tags)
            {
                if (!_tags.Contains(tag)) _tags.Add(tag);
            }
            
            foreach(var tag in e.Tags)
            {
                if (!_tagsMap[e.Id].Contains(tag)) _tagsMap[e.Id].Add(tag);
            }
        }

        private readonly List<BookPreview> _bookPreviews = new List<BookPreview>();
        private readonly List<Book> _books = new List<Book>();
        private readonly Dictionary<Guid, List<string>> _tagsMap = new Dictionary<Guid, List<string>>();
        private readonly List<string> _tags = new List<string>();

        #endregion

        public class BookPreview
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class Book
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public List<Review> Reviews { get; private set; } = new List<Review>();
            public int Rating { get; set; }
            public List<string> Tags { get; set; }
        }

        public class Review
        {
            public string Reader { get; set; }
            public int Rating { get; set; }
            public string Comment { get; set; }
        }
    }
}