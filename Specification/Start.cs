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
           IWant = "I want to take available book for reading",
           SoThat = "So that I can start reading the book")]
    [TestFixture]
    public class Start
    {
        private ReadOne _app;

        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }
        [Test]
        public void StartReading()
        {
            this.Given(x => x.BookExists())
                    .And(x => x.BookIsNotStartedByYou())
                .When(x => x.NeoSaysStart())
                .Then(x => x.NeoStartedBook())
                .BDDfy();
        }
        [Test]
        public void StartReadingNotAvailable()
        {
            this.Given(x => x.BookExists())
                    .And(x => x.BookIsAlreadyStartedByYou())
                .When(x => x.NeoSaysStart())
                .Then(x => x.NeoIsAlreadyStartedBook())
                .BDDfy();
        }
        [Test]
        public void StartReadingFailed()
        {
            this.Given(x => x.BookNotExists())
                .When(x => x.NeoSaysStart())
                .Then(x => x.NeoSeesBookNotExists())
                .BDDfy();
        }
        private void BookExists()
        {
            throw new NotImplementedException();
        }
        private void BookNotExists()
        {
            throw new NotImplementedException();
        }
        private void BookIsNotStartedByYou()
        {
            throw new NotImplementedException();
        }
        private void BookIsAlreadyStartedByYou()
        {
            throw new NotImplementedException();
        }
        private void NeoSaysStart()
        {
            throw new NotImplementedException();
        }
        private void NeoStartedBook()
        {
            throw new NotImplementedException();
        }
        private void NeoIsAlreadyStartedBook()
        {
            throw new NotImplementedException();
        }

        private void NeoSeesBookNotExists()
        {
            throw new NotImplementedException();
        }
    }
}
