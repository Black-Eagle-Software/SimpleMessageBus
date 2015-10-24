using System;
using System.Collections.Generic;

namespace SimpleMessageBus {
    public class MessageBus {
        #region  StaticFieldsConstants

        private static MessageBus _instance;

        #endregion

        #region Fields

        private readonly Dictionary<Type, List<WeakAction>> _recipients;

        #endregion

        #region Constructors

        protected MessageBus() {
            this._recipients = new Dictionary<Type, List<WeakAction>>();
        }

        #endregion

        #region Properties

        public static MessageBus Instance => _instance ?? (_instance = new MessageBus());

        #endregion

        #region Methods

        public void Register<TMessage>(object recipient, Action<TMessage> action) {
            var mType = typeof(TMessage);
            if (!mType.IsImplementationOf<IMessage>()) {
                throw new ArgumentException("Action must require class implementing IMessage interface as a parameter.", mType.Name);
            }
            if (!this._recipients.ContainsKey(mType)) { this._recipients[mType] = new List<WeakAction>(); }
            this._recipients[mType].Add(new WeakAction<TMessage>(recipient, action));
        }

        public void SendMessage<TMessage>(TMessage msg) {
            var mType = typeof(TMessage);
            if (!mType.IsImplementationOf<IMessage>()) {
                throw new ArgumentException("Message type must implement IMessage interface.", mType.Name);
            }
            if (!this._recipients.ContainsKey(mType)) {
                throw new ArgumentException($"There is no recipient registered to recieve messages of type: {mType.Name}.");
            }
            var subList = this._recipients[mType];
            foreach (var t in subList) { ((WeakAction<TMessage>)t).Execute(msg); }
        }

        public void UnRegister<TMessage>(object recipient) {
            var mType = typeof(TMessage);
            if (!this._recipients.ContainsKey(mType)) { return; }
            var subList = this._recipients[mType];
            var index = -1;
            for (var i = 0; i < subList.Count; i++) {
                if (!((WeakAction<TMessage>)subList[i]).Reference.Target.Equals(recipient)) { continue; }
                index = i;
                break;
            }
            if (index == -1) { throw new ArgumentOutOfRangeException(recipient.ToString(), "Attempted to UnRegister an object that was not previously registered to recieve messages."); }
            this._recipients[mType].RemoveAt(index);
        }

        #endregion
    }
}