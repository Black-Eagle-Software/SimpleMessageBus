using System;
using System.Reflection;

namespace SimpleMessageBus {
    internal class WeakAction {
        #region Constructors

        protected WeakAction() {}

        internal WeakAction(object target, Action action) {
            if (target != null) { this.Reference = new WeakReference(target); }
            this.Method = action.Method;
            this.ActionReference = new WeakReference(action.Target);
        }

        #endregion

        #region Properties

        internal MethodInfo Method { get; set; }
        internal WeakReference ActionReference { get; set; }
        internal WeakReference Reference { get; set; }
        internal bool IsAlive => this.Reference.IsAlive;

        #endregion

        #region Methods

        internal void Execute() {
            var actionTarget = this.ActionReference.Target;
            if (!this.IsAlive) { return; }
            if (this.Method != null && this.ActionReference != null && actionTarget != null) { this.Method.Invoke(actionTarget, null); }
        }

        #endregion
    }

    internal class WeakAction<T> : WeakAction {
        #region Constructors

        internal WeakAction(object target, Action<T> action) {
            if (target != null) { this.Reference = new WeakReference(target); }
            this.Method = action.Method;
            this.ActionReference = new WeakReference(action.Target);
        }

        #endregion

        #region Methods

        internal new void Execute() {
            this.Execute(default(T));
        }

        internal void Execute(T parameter) {
            var actionTarget = this.ActionReference.Target;
            if (!this.IsAlive) { return; }
            if (this.Method != null && this.ActionReference != null && actionTarget != null) { this.Method.Invoke(actionTarget, new object[] {parameter}); }
        }

        #endregion
    }
}