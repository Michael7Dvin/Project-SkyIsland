using DG.Tweening;
using UnityEngine;

namespace UI.AnimatedElements
{
    [CreateAssetMenu(menuName = "Configs/UI/Animated Elements/Button", fileName = "Animated Button Config")]
    public class AnimatedButtonConfig : ScriptableObject
    {
        public int BumpLoopsCount => 2;
        [field: SerializeField] public float BumpScale { get; private set; }
        [field: SerializeField] public float BumpDuration { get; private set; }
        [field: SerializeField] public Ease BumpEase { get; private set; }
        [field: SerializeField] public LoopType BumpLoopType { get; private set; }
    }
}