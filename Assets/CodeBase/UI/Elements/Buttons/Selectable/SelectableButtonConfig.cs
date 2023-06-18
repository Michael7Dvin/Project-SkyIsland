using TMPro;
using UI.Animators.OnClickScaler;
using UnityEngine;

namespace UI.Elements.Buttons.Selectable
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Selectable Button", fileName = "Selectable Button")]
    public class SelectableButtonConfig : ScriptableObject
    {
        [field: SerializeField] public OnClickScalerConfig OnClickScalerConfig { get; private set; }
        [field: SerializeField] public TMP_ColorGradient SelectedColorGradient { get; private set; }
        [field: SerializeField] public TMP_ColorGradient UnselectedColorGradient { get; private set; }
    }
}