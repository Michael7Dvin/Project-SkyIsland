using System;
using UnityEngine;

namespace UI.Windows.Base
{
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        public abstract WindowType Type { get; }
        public bool IsActive { get; private set; }
        
        public event Action<IWindow> Destroyed;

        private void Awake() =>
            Enable();

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
            UnsubscribeFromButtons();
        }

        public void Enable()
        {
            SubscribeOnButtons();
            gameObject.SetActive(true);
            IsActive = true;
        }

        public void Disable()
        {
            UnsubscribeFromButtons();
            gameObject.SetActive(false);
            IsActive = false;
        }

        protected abstract void SubscribeOnButtons();
        protected abstract void UnsubscribeFromButtons();
    }
}