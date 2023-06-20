using DG.Tweening;
using UnityEngine;

namespace UI.Animators.WindowMover
{
    [CreateAssetMenu(menuName = "Configs/UI/Animators/Window Mover", fileName = "Window Mover")]
    public class WindowMoverConfig : ScriptableObject
    {
        [Header("On Window Opened")] 
        [SerializeField] private float _onOpenedDelay;
        [SerializeField] private Vector2 _onOpenedStartPosition;
        [SerializeField] private float _onOpenedDuration;
        [SerializeField] private Ease _onOpenedEase;

        [Header("On Window Closed")]
        [SerializeField] private float _onClosedDelay;
        [SerializeField] private Vector2 _onClosedEndPosition;
        [SerializeField] private float _onClosedDuration;
        [SerializeField] private Ease _onClosedEase;

        public float OnOpenedDelay => _onOpenedDelay;
        public Vector2 OnOpenedStartPosition => _onOpenedStartPosition;
        public float OnOpenedDuration => _onOpenedDuration;
        public Ease OnOpenedEase => _onOpenedEase;
        
        public float OnClosedDelay => _onClosedDelay;
        public Vector2 OnClosedEndPosition => _onClosedEndPosition;
        public float OnClosedDuration => _onClosedDuration;
        public Ease OnClosedEase => _onClosedEase;
    }
}