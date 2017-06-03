using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace ReadOne.Application
{
    [Story(AsA = "As a reader",
           IWant = "I want to see available books",
           SoThat = "So that I can choose some book for reading")]
    [TestFixture]
    public class SeeAvailableBooks
    {
        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }

        [Test]
        public void ShowListOfBooks()
        {
            this.Given(x => x.ListOfBooksIsNotEmpty())
                .When(x => x.NeoSaysBooks())
                .Then(x => x.HeSeesListOfBooks())
                .BDDfy();
        }

        [Test]
        public void NotShowListOfBooks()
        {
            this.Given(x => x.ListOfBooksIsEmpty())
                .When(x => x.NeoSaysBooks())
                .Then(x => x.HeSeesNoAvailableBooks())
                .BDDfy();
        }

        private void ListOfBooksIsNotEmpty()
        {
            _addedBookId = Guid.NewGuid();
            _app.Do(new Add
            {
                Id = _addedBookId,
                Name = "name"
            });
        }

        private void ListOfBooksIsEmpty()
        {
        }

        private void NeoSaysBooks()
        {
            _previews = _app.Books(new string[0]);
        }

        private void HeSeesListOfBooks()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_previews.Length, Is.EqualTo(1));
                Assert.That(_previews[0].Id, Is.EqualTo(_addedBookId));
                Assert.That(_previews[0].Name, Is.EqualTo("name"));
            });
        }

        private void HeSeesNoAvailableBooks()
        {
            Assert.That(_previews.Length, Is.EqualTo(0));
        }

        private ReadOne _app;
        private Library.BookPreview[] _previews;
        private Guid _addedBookId;
    }
}