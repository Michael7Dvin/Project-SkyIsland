using UnityEngine;

namespace UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        private void Awake() => 
            SubscribeOnButtons();

        private void OnDestroy() => 
            UnsubscribeFromButtons();

        protected void CloseWindow() => 
            Destroy(gameObject);

        protected abstract void SubscribeOnButtons();
        protected abstract void UnsubscribeFromButtons();
    }
}