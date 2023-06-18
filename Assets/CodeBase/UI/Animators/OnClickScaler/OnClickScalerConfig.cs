using DG.Tweening;
using UnityEngine;

namespace UI.Animators.OnClickScaler
{
    [CreateAssetMenu(menuName = "Configs/UI/Animators/On Click Scaler", fileName = "On Click Scaler")]
    public class OnClickScalerConfig : ScriptableObject
    {
        [Header("On Pointer Down")]
        [SerializeField] private Vector3 _onDownEndScale;
        [SerializeField] private float _onDownDuration;
        [SerializeField] private Ease _onDownEase;

        [Header("On Pointer Up")]
        [SerializeField] private Vector3 _onUpEndScale;
        [SerializeField] private float _onUpDuration;
        [SerializeField] private Ease _onUpEase;

        public Vector3 DefaultScale => Vector3.one;

        public Vector3 OnDownEndScale => _onDownEndScale;
        public float OnDownDuration => _onDownDuration;
        public Ease OnDownEase => _onDownEase;

        public Vector3 OnUpEndScale => _onUpEndScale;
        public float OnUpDuration => _onUpDuration;
        public Ease OnUpEase => _onUpEase;
    }
}