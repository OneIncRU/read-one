using NUnit.Framework;
using TestStack.BDDfy;

namespace ReadOne.Application
{
    [Story(AsA = "As a reader",
           IWant = "I want to see available books",
           SoThat = "So that I can overview all available books")]
    [TestFixture]
    public class ShowMeBooks
    {
        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }
        [Test]
        public void ShowListOfBooks()
        {
            this.When(x => x.BobSaysShowMeBooks())
                    .And(x => x.ListOfBooksIsNotEmpty())
                .Then(x => x.HeSeesListOfBooks())
                .BDDfy();
        }
        [Test]
        public void NotShowListOfBooks()
        {
            this.When(x => x.BobSaysShowMeBooks())
                    .And(x => x.ListOfBooksIsEmpty())
                .Then(x => x.HeSeesNoAvailableBooks())
                .BDDfy();
        }
        private void BobSaysShowMeBooks()
        {
        }
        private void ListOfBooksIsNotEmpty()
        {
        }
        private void ListOfBooksIsEmpty()
        {
        }
        private void HeSeesListOfBooks()
        {
        }
        private void HeSeesNoAvailableBooks()
        {
        }
        private ReadOne _app;
    }
}