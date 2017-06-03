﻿using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace ReadOne.Application
{
    [Story(AsA = "As a reader",
           IWant = "I want to know all comands to control",
           SoThat = "So that I can overview all available comands")]
    [TestFixture]
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
            Assert.AreEqual("Hi", _response);
        }

        private void BobSaysAnything()
        {
            _message = "sjhgad";
        }

        private ReadOne _app;
        private string _message = string.Empty;
        private string _response = string.Empty;
    }
}