using System;
using System.Threading;
using UnityEngine;

namespace UI.Windows.Base.WindowView
{
    public abstract class BaseWindowView : MonoBehaviour, IWindowView
    {
        public event Action Destroyed;

        private void OnEnable()
        {
            EnableAnimators();
            SubscribeControls();
        }

        private void OnDisable()
        {
            DisableAnimators();
            UnsubscribeControls();
        }

        private void OnDestroy() => 
            Destroyed?.Invoke();

        public virtual void Open() => 
            gameObject.SetActive(true);

        public virtual void Close() => 
            gameObject.SetActive(false);

        protected abstract void SubscribeControls();
        protected abstract void UnsubscribeControls();

        protected abstract void EnableAnimators();
        protected abstract void DisableAnimators();
    }
}