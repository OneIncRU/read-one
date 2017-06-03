using System;
using NUnit.Framework;
using TestStack.BDDfy;

namespace ReadOne.Application
{
    [Story(AsA = "As a reader",
           IWant = "I want to see certain book info",
           SoThat = "So that I can know what parameters the book has")]
    [TestFixture]
    public class BookInformation
    {
        private ReadOne _app;

        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }
        [Test]
        public void GetAvailableBookInfo()
        {
            this.Given(x => x.CorrectChoiceOfBook())
                .When(x => x.NeoSaysBookInfo())
                .Then(x => x.HeSeesBookInfo())
                .BDDfy();
        }
        [Test]
        public void GetNotAvailableBookInfo()
        {
            this.Given(x => x.IncorrectChoiceOfBook())
                .When(x => x.NeoSaysBookInfo())
                .Then(x => x.HeSeesNoBookInfo())
                .BDDfy();
        }
        
        private void CorrectChoiceOfBook()
        {
            _bookId = Guid.NewGuid();
            _bookName = "Martin Fowler 'Refactoring: Improving the Design of Existing Code'";
            _app.Do(new Add
            {
                Id = _bookId,
                Name = _bookName
            });
        }

        private void IncorrectChoiceOfBook()
        {
            _bookId = Guid.NewGuid();
        }

        private void NeoSaysBookInfo()
        {
            _bookInfo = _app.BookInfo(_bookId);
        }

        private void HeSeesBookInfo()
        {
            Assert.That(_bookInfo, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(_bookInfo.Id, Is.EqualTo(_bookId));
                Assert.That(_bookInfo.Name, Is.EqualTo(_bookName));
            });
        }

        private void HeSeesNoBookInfo()
        {
            Assert.That(_bookInfo, Is.Null);
        }

        private Guid _bookId;
        private Library.Book _bookInfo;
        private string _bookName;
    }
}
