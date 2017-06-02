using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace ReadOne.Specification
{
    [Story(AsA = "As a reader",
           IWant = "I want to list all books",
           SoThat = "So that I can overview all library content")]
    public class LibraryOverview
    {
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
    }
}