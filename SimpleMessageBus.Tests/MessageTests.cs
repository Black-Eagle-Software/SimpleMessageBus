using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleMessageBus.Tests {
    [TestClass]
    public class MessageTests {
        [TestMethod]
        public void MessageTests_RegisterIMessageMessageType() {
            //this should register without raising exception
            MessageBus.Instance.Register<MockMessage>(this, msg => { });
            MessageBus.Instance.UnRegister<MockMessage>(this);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MessageTests_RegisterNonIMessageMessageType() {
            //this should throw an argument exception
            MessageBus.Instance.Register<MockNotMessage>(this, msg => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MessageTests_SendMessageNoRecipient() {
            MessageBus.Instance.SendMessage(new MockMessage());
        }
    }
}
