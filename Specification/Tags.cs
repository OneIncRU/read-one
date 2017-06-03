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
           IWant = "I want to see available tags for sorting books",
           SoThat = "So that I can overview all available tags")]
    [TestFixture]
    public class Tags
    {
        private ReadOne _app;

        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }
        [Test]
        public void GetNotEmptyTagList()
        {
            this.Given(x => x.TagListIsNotEmpty())
                .When(x => x.NeoSaysTags())
                .Then(x => x.HeSeesTagList())
                .BDDfy();
        }
        [Test]
        public void GetEmptyTagList()
        {
            this.Given(x => x.TagListIsEmpty())
                .When(x => x.NeoSaysTags())
                .Then(x => x.HeSeesEmptyTagList())
                .BDDfy();
        }
        private void NeoSaysTags()
        {
            throw new NotImplementedException();
        }
        private void TagListIsNotEmpty()
        {
            throw new NotImplementedException();
        }
        private void HeSeesTagList()
        {
            throw new NotImplementedException();
        }
        private void TagListIsEmpty()
        {
            throw new NotImplementedException();
        }
        private void HeSeesEmptyTagList()
        {
            throw new NotImplementedException();
        }
    }
}
