using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace ReadOne.Application
{
    [Story(AsA = "As a reader",
           IWant = "I want to know all comands to control",
           SoThat = "So that I can overview all available comands")]
    public class ComandList
    {
        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }

        [Test]
        public void BobIsGreeted()
        {
            this.When(x => x.BobSaysAnything())
                .Then(x => x.HeSeesHi())
                .BDDfy();
        }

        private void HeSeesHi()
        {
            throw new NotImplementedException();
        }

        private void BobSaysAnything()
        {
            throw new NotImplementedException();
        }

        private ReadOne _app;
    }
}
