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
           IWant = "I want to filter available books by some tag",
           SoThat = "So that I can overview all available books having some tag")]
    [TestFixture]
    public class BooksByTag
    {
        [SetUp]
        public void SetUp()
        {
            _app = new ReadOne();
        }
        [Test]
        public void GetBooksByCorrectTag()
        {
            this.Given(x => x.InputedTagIsCorrect())
                .When(x => x.NeoSaysBooksByTag())
                .Then(x => x.NeoSeesFilteredByTagBooks())
                .BDDfy();
        }
        [Test]
        public void GetBooksByIncorrectTag()
        {
            this.Given(x => x.InputTagIsIncorrect())
                .When(x => x.NeoSaysBooksByTag())
                .Then(x => x.NeoSeesEnterCorrectTag())
                .BDDfy();
        }
        private void InputedTagIsCorrect()
        {
            throw new NotImplementedException();
        }
        private void InputTagIsIncorrect()
        {
            throw new NotImplementedException();
        }
        private void NeoSaysBooksByTag()
        {
            throw new NotImplementedException();
        }
        private void NeoSeesFilteredByTagBooks()
        {
            throw new NotImplementedException();
        }
        private void NeoSeesEnterCorrectTag()
        {
            throw new NotImplementedException();
        }
    }
}
