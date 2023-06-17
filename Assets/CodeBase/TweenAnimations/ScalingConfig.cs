using DG.Tweening;
using UnityEngine;

namespace TweenAnimations
{
    [CreateAssetMenu(menuName = "Configs/Tween Animations/Scaling", fileName = "Scaling Animation Config")]
    public class ScalingConfig : ScriptableObject
    {
        [field: SerializeField] public float EndScale { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
        [field: SerializeField] public int LoopsCount { get; private set; }
        [field: SerializeField] public LoopType LoopType { get; private set; }
    }
}