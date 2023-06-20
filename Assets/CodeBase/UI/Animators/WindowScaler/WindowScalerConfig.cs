using DG.Tweening;
using UnityEngine;

namespace UI.Animators.WindowScaler
{
    [CreateAssetMenu(menuName = "Configs/UI/Animators/Window Scaler", fileName = "Window Scaler")]
    public class WindowScalerConfig : ScriptableObject
    {
        [Header("On Window Opened")]
        [SerializeField] private float _onOpenedDelay;
        [SerializeField] private Vector3 _onOpenedStartScale;
        [SerializeField] private float _onOpenedDuration;
        [SerializeField] private Ease _onOpenedEase;

        [Header("On Window Closed")]
        [SerializeField] private float _onClosedDelay;
        [SerializeField] private Vector3 _onClosedEndScale;
        [SerializeField] private float _onClosedDuration;
        [SerializeField] private Ease _onClosedEase;

        public Vector3 DefaultScale => Vector3.one;

        public float OnOpenedDelay => _onOpenedDelay;
        public Vector3 OnOpenedStartScale => _onOpenedStartScale;
        public float OnOpenedDuration => _onOpenedDuration;
        public Ease OnOpenedEase => _onOpenedEase;
        
        public float OnClosedDelay => _onClosedDelay;
        public Vector3 OnClosedEndScale => _onClosedEndScale;
        public float OnClosedDuration => _onClosedDuration;
        public Ease OnClosedEase => _onClosedEase;
    }
}