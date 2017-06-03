using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace ReadOne.Application
{
    [Story(AsA = "As a reader",
           IWant = "I want to overview all available comands",
           SoThat = "So that I could know how to control the Bot by comands")]
    [TestFixture]
    public class Comands
    {
        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }

        [Test]
        public void NeoIsGreeted()
        {
            this.When(x => x.NeoSaysAnything())
                    .And(x => x.NeoSaysNotOneFromListComands())
                .Then(x => x.HeSeesHi())
                .BDDfy();
        }
        private void NeoSaysNotOneFromListComands()
        {

        }
        private void HeSeesHi()
        {
//            Assert.AreEqual("Books, Tag, BooksByTag, BooksByTags, BookInfo#, Start, Finish, Review, Add, Remove", _response);
        }

        private void NeoSaysAnything()
        {
            _message = "sjhgad";
        }

        private ReadOne _app;
        private string _message = string.Empty;
        private string _response = string.Empty;
    }
}
