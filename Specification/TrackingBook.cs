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
           IWant = "I want to track books that I read",
           SoThat = "So that I can make remarks for memory and also share my reviews about books with others")]
    [TestFixture]
    public class Start : BaseBook
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
            this.Given(x => x.BookDoesNotExist())
                .When(x => x.NeoSaysStart())
                .Then(x => x.NeoSeesBookNotExists())
                .BDDfy();
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
    public class Finish : BaseBook
    {
        private ReadOne _app;

        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }
        [Test]
        public void FinishReading()
        {
            this.Given(x => x.BookExists())
                    .And(x => x.BookIsAlreadyStartedByYou())
                .When(x => x.NeoSaysFinish())
                .Then(x => x.NeoFinishedBook())
                .BDDfy();
        }
        [Test]
        public void FinishReadingNotAvailable()
        {
            this.Given(x => x.BookExists())
                    .And(x => x.BookIsNotStartedByYou())
                .When(x => x.NeoSaysFinish())
                .Then(x => x.NeoIsNotStartedBook())
                .BDDfy();
        }
        [Test]
        public void FinishReadingFailed()
        {
            this.Given(x => x.BookDoesNotExist())
                .When(x => x.NeoSaysFinish())
                .Then(x => x.NeoSeesBookNotExists())
                .BDDfy();
        }

        private void NeoSaysFinish()
        {
            throw new NotImplementedException();
        }
        private void NeoFinishedBook()
        {
            throw new NotImplementedException();
        }
        private void NeoIsNotStartedBook()
        {
            throw new NotImplementedException();
        }

        private void NeoSeesBookNotExists()
        {
            throw new NotImplementedException();
        }
    }
    public class Review : BaseBook
    {
            private ReadOne _app;

            [SetUp]
            public void SetUp()
            {
                _app = new ReadOne();
            }
            [Test]
            public void ReviewBook()
            {
                this.Given(x => x.BookExists())
                    .When(x => x.NeoSaysReview())
                    .Then(x => x.NeoReviewedBook())
                    .BDDfy();
            }
            [Test]
            public void ReviewBookFailed()
            {
                this.Given(x => x.BookDoesNotExist())
                    .When(x => x.NeoSaysReview())
                    .Then(x => x.NeoSeesBookNotExists())
                    .BDDfy();
            }

            private void NeoSaysReview()
            {
                throw new NotImplementedException();
            }
            private void NeoReviewedBook()
            {
                throw new NotImplementedException();
            }

            private void NeoSeesBookNotExists()
            {
                throw new NotImplementedException();
            }
     }
}
