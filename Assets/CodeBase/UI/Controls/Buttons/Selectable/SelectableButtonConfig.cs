using TMPro;
using UI.Animators.OnClickScaler;
using UI.Animators.OnSelectScaler;
using UnityEngine;

namespace UI.Controls.Buttons.Selectable
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Selectable Button", fileName = "Selectable Button")]
    public class SelectableButtonConfig : ScriptableObject
    {
        [field: SerializeField] public OnSelectScalerConfig OnSelectScalerConfig { get; private set; }
        [field: SerializeField] public OnClickScalerConfig OnClickScalerConfig { get; private set; }
        [field: SerializeField] public TMP_ColorGradient OnSelectColorGradient { get; private set; }
        [field: SerializeField] public TMP_ColorGradient OnUnselectColorGradient { get; private set; }
    }
}