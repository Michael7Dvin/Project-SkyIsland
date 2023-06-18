using DG.Tweening;
using UnityEngine;

namespace UI.Animators.OnClickScaler
{
    [CreateAssetMenu(menuName = "Configs/UI/Tween Animators/On Click Scaler", fileName = "On Click Scaler")]
    public class OnClickScalerConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 DefaultScale { get; private set; }

        [Header("On Pointer Down")]
        [SerializeField] private Vector3 _onDownEndScale;
        [SerializeField] private float _onDownDuration;
        [SerializeField] private Ease _onDownEase;
        [SerializeField] private int _onDownLoopsCount;
        [SerializeField] private LoopType _onDownLoopType;

        [Header("On Pointer Up")]
        [SerializeField] private Vector3 _onUpEndScale;
        [SerializeField] private float _onUpDuration;
        [SerializeField] private Ease _onUpEase;
        [SerializeField] private int _onUpLoopsCount;
        [SerializeField] private LoopType _onUpLoopType;
        
        public Vector3 OnDownEndScale => _onDownEndScale;
        public float OnDownDuration => _onDownDuration;
        public Ease OnDownEase => _onDownEase;
        public int OnDownLoopsCount => _onDownLoopsCount;
        public LoopType OnDownLoopType => _onDownLoopType;
        
        public Vector3 OnUpEndScale => _onUpEndScale;
        public float OnUpDuration => _onUpDuration;
        public Ease OnUpEase => _onUpEase;
        public int OnUpLoopsCount => _onUpLoopsCount;
        public LoopType OnUpLoopType => _onUpLoopType;
    }
}