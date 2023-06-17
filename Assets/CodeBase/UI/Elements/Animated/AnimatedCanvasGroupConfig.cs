using UnityEngine;

namespace UI.Elements.Animated
{
    [CreateAssetMenu(menuName = "Configs/UI/Animated Elements/Canvas Group", fileName = "Animated Canvas Group Config")]
    public class AnimatedCanvasGroupConfig : ScriptableObject
    {
        public float MinAlpha => 0f;
        public float MaxAlpha => 1f;
        [field: SerializeField] public float FadeDuration { get; private set; }
    }
}