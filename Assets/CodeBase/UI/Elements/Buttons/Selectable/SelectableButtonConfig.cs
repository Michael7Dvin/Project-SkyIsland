using TMPro;
using TweenAnimations;
using UnityEngine;

namespace UI.Elements.Buttons.Selectable
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Selectable Button", fileName = "Selectable Button")]
    public class SelectableButtonConfig : ScriptableObject
    {
        [field: SerializeField] public TMP_ColorGradient SelectedColorGradient { get; private set; }
        [field: SerializeField] public TMP_ColorGradient UnselectedColorGradient { get; private set; }
        [field: SerializeField] public ScalingConfig OnButtonDownScalingConfig { get; private set; }
        [field: SerializeField] public ScalingConfig OnButtonUpScalingConfig { get; private set; }
    }
}