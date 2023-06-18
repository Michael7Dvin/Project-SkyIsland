using DG.Tweening;
using UnityEngine;

namespace UI.Animators.WindowScaler
{
    [CreateAssetMenu(menuName = "Configs/UI/Animators/Window Scaler", fileName = "Window Scaler")]
    public class WindowScalerConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 DefaultScale { get; private set; }

        [Header("On Window Opened")]
        [SerializeField] private float _onOpenDelay;
        [SerializeField] private Vector3 _onOpenedStartScale;
        [SerializeField] private Vector3 _onOpenedEndScale;
        [SerializeField] private float _onOpenedDuration;
        [SerializeField] private Ease _onOpenedEase;

        [Header("On Window Closed")]
        [SerializeField] private float _onCloseDelay;
        [SerializeField] private Vector3 _onClosedEndScale;
        [SerializeField] private float _onClosedDuration;
        [SerializeField] private Ease _onClosedEase;

        public float OnOpenDelay => _onOpenDelay;
        public Vector3 OnOpenedStartScale => _onOpenedStartScale;
        public Vector3 OnOpenedEndScale => _onOpenedEndScale;
        public float OnOpenedDuration => _onOpenedDuration;
        public Ease OnOpenedEase => _onOpenedEase;
        

        public float OnCloseDelay => _onCloseDelay;
        public Vector3 OnClosedEndScale => _onClosedEndScale;
        public float OnClosedDuration => _onClosedDuration;
        public Ease OnClosedEase => _onClosedEase;
    }
}