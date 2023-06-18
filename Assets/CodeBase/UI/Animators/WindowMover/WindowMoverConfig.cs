using DG.Tweening;
using UnityEngine;

namespace UI.Animators.WindowMover
{
    
    [CreateAssetMenu(menuName = "Configs/UI/Animators/Window Mover", fileName = "Window Mover")]
    public class WindowMoverConfig : ScriptableObject
    {
        [Header("On Window Opened")] 
        [SerializeField] private float _onOpenDelay;
        [SerializeField] private Vector2 _onOpenStartPosition;
        [SerializeField] private float _onOpenDuration;
        [SerializeField] private Ease _onOpenEase;

        [Header("On Window Closed")]
        [SerializeField] private float _onCloseDelay;
        [SerializeField] private Vector2 _onClosedEndPosition;
        [SerializeField] private float _onClosedDuration;
        [SerializeField] private Ease _onClosedEase;

        public float OnOpenDelay => _onOpenDelay;
        public Vector2 OnOpenStartPosition => _onOpenStartPosition;
        public float OnOpenDuration => _onOpenDuration;
        public Ease OnOpenEase => _onOpenEase;


        public float OnCloseDelay => _onCloseDelay;
        public Vector2 OnClosedEndPosition => _onClosedEndPosition;
        public float OnClosedDuration => _onClosedDuration;
        public Ease OnClosedEase => _onClosedEase;
    }
}