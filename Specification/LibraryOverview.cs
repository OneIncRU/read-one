using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace ReadOne.Specification
{
    [Story(AsA = "As a reader",
           IWant = "I want to view all books",
           SoThat = "So that I can overview all library content")]
    public class LibraryOverview
    {
        [Test]
        public void ListOfBooks()
        {
            this.When(x => x.ShowMeBooks())
                  .And(x => x.ListOfBooksIsNotEmpty())
                .Then(x => x.YouSeesListOfBooks())
                .BDDfy();
        }

        private void YouSeesListOfBooks()
        {
            throw new NotImplementedException();
        }

        private void ShowMeBooks()
        {
            throw new NotImplementedException();
        }
        
        private void ListOfBooksIsNotEmpty()
        {
            throw new NotImplementedException();
        }
    }
}
