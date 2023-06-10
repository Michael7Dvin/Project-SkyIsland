using System;
using UnityEngine;

namespace UI.Windows.Base
{
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        public abstract WindowType Type { get; }
        public event Action<IWindow> Closed;

        private void Awake() => 
            SubscribeOnButtons();

        private void OnDestroy()
        {
            Closed?.Invoke(this);
            UnsubscribeFromButtons();
        }

        public void Close() => 
            Destroy(gameObject);

        protected abstract void SubscribeOnButtons();
        protected abstract void UnsubscribeFromButtons();
    }
}