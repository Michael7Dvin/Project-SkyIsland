using UnityEngine;

namespace UI.Elements
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Fading Canvas Group", fileName = "Fading Canvas Group")]
    public class FadingCanvasGroupConfig : ScriptableObject
    {
        public float MinAlpha => 0f;
        public float MaxAlpha => 1f;
        [field: SerializeField] public float FadeDuration { get; private set; }
    }
}