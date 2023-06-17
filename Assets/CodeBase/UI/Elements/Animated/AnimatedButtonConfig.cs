using DG.Tweening;
using UnityEngine;

namespace UI.Elements.Animated
{
    [CreateAssetMenu(menuName = "Configs/UI/Animated Elements/Button", fileName = "Animated Button Config")]
    public class AnimatedButtonConfig : ScriptableObject
    {
        [Header("On Button Down")]
        [SerializeField] private float _onDownScale;
        [SerializeField] private float _onDownDuration;
        [SerializeField] private Ease _onDownEase;
        [SerializeField] private int _onDownLoopsCount;
        [SerializeField] private LoopType _onDownLoopType;

        [Header("On Button Up")]
        [SerializeField] private float _onUpScale;
        [SerializeField] private float _onUpDuration;
        [SerializeField] private Ease _onUpEase;
        [SerializeField] private int _onUpLoopsCount;
        [SerializeField] private LoopType _onUpLoopType;
        
        public float OnDownScale => _onDownScale;
        public float OnDownDuration => _onDownDuration;
        public Ease OnDownEase => _onDownEase;
        public int OnDownLoopsCount => _onDownLoopsCount;
        public LoopType OnDownLoopType => _onDownLoopType;
        
        public float OnUpScale => _onUpScale;
        public float OnUpDuration => _onUpDuration;
        public Ease OnUpEase => _onUpEase;
        public int OnUpLoopsCount => _onUpLoopsCount;
        public LoopType OnUpLoopType => _onUpLoopType;
    }
}