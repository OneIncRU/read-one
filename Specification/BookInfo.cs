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
           IWant = "I want to see certain book info",
           SoThat = "So that I can overview chosen book info")]
    [TestFixture]
    public class BookInfo
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
            throw new NotImplementedException();
        }
        private void IncorrectChoiceOfBook()
        {
            throw new NotImplementedException();
        }
        private void NeoSaysBookInfo()
        {
            throw new NotImplementedException();
        }
        private void HeSeesBookInfo()
        {
            throw new NotImplementedException();
        }
        private void HeSeesNoBookInfo()
        {
            throw new NotImplementedException();
        }
    }
}
