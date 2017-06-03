using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace ReadOne.Application
{
    [Story(AsA = "As a reader",
           IWant = "I want to edit library: add and remove books",
           SoThat = "So that I can actualize the library")]
    [TestFixture]
    public class LibraryManagement
    {
        private ReadOne _app;

        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }
        [Test]
        public void AddBook()
        {
            this.When(x => x.NeoSaysAdd())
                .Then(x => x.NeoGoToAddBook())
                .BDDfy();
        }

        private void NeoSaysAdd()
        {
            throw new NotImplementedException();
        }
        private void NeoGoToAddBook()
        {
            throw new NotImplementedException();
        }
    }
    public class Remove : BaseBook
    {
        private ReadOne _app;

        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }
        [Test]
        public void RemoveBook()
        {
            this.Given(x => x.BookExists())
                    .And(x => x.BookIsNotStartedByAnyUser())
                .When(x => x.NeoSaysRemove())
                .Then(x => x.NeoRemovedBook())
                .BDDfy();
        }
        [Test]
        public void RemovingBookNotAvailable()
        {
            this.Given(x => x.BookExists())
                    .And(x => x.BookIsStartedByAnyUser())
                .When(x => x.NeoSaysRemove())
                .Then(x => x.NeoSeesBookIsStartedByAnyUser())
                .BDDfy();
        }
        [Test]
        public void FinishReadingFailed()
        {
            this.Given(x => x.BookNotExists())
                .When(x => x.NeoSaysRemove())
                .Then(x => x.NeoSeesBookNotExists())
                .BDDfy();
        }

        private void NeoSaysRemove()
        {
            throw new NotImplementedException();
        }
        private void NeoRemovedBook()
        {
            throw new NotImplementedException();
        }
        private void NeoSeesBookIsStartedByAnyUser()
        {
            throw new NotImplementedException();
        }

        private void NeoSeesBookNotExists()
        {
            throw new NotImplementedException();
        }
    }
}
