using DG.Tweening;
using UnityEngine;

namespace UI.Animators.OnSelectScaler
{
    [CreateAssetMenu(menuName = "Configs/UI/Animators/On Select Scaler", fileName = "On Select Scaler")]
    public class OnSelectScalerConfig : ScriptableObject
    {
        [Header("On Pointer Enter")]
        [SerializeField] private Vector3 _onEnterEndScale;
        [SerializeField] private float _onEnterDuration;
        [SerializeField] private Ease _onEnterEase;
        
        [Header("On Pointer Exit")]
        [SerializeField] private Vector3 _onExitEndScale;
        [SerializeField] private float _onExitDuration;
        [SerializeField] private Ease _onExitEase;
        
        public Vector3 DefaultScale => Vector3.one;
        
        public Vector3 OnEnterEndScale => _onEnterEndScale;
        public float OnEnterDuration => _onEnterDuration;
        public Ease OnEnterEase => _onEnterEase;
        
        public Vector3 OnExitEndScale => _onExitEndScale;
        public float OnExitDuration => _onExitDuration;
        public Ease OnExitEase => _onExitEase;
    }
}