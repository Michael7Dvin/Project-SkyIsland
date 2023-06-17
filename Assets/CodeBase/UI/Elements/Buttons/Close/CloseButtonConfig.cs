using TweenAnimations;
using UnityEngine;

namespace UI.Elements.Buttons.Close
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Close Button", fileName = "Close Button")]
    public class CloseButtonConfig : ScriptableObject
    {
        [field: SerializeField] public ScalingConfig OnButtonDownScalingConfig { get; private set; }
        [field: SerializeField] public ScalingConfig OnButtonUpScalingConfig { get; private set; }
    }
}