using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace ReadOne.Application
{
    [Story(AsA = "As a reader",
           IWant = "I want to see available books",
           SoThat = "So that I can overview all available books")]
    [TestFixture]
    public class Books
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
        private void NeoSaysBooks()
        {
            throw new NotImplementedException();
        }

        private void ListOfBooksIsNotEmpty()
        {
            throw new NotImplementedException();
        }

        private void ListOfBooksIsEmpty()
        {
            throw new NotImplementedException();
        }

        private void HeSeesListOfBooks()
        {
            throw new NotImplementedException();
        }

        private void HeSeesNoAvailableBooks()
        {
            throw new NotImplementedException();
        }

        private ReadOne _app;
    }
}