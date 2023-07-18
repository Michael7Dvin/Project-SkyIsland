using TMPro;
using UI.Animators.OnClickScaler;
using UnityEngine;

namespace UI.Controls.Buttons.SaveSlots
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Save Slot Button", fileName = "Save Slot Button")]
    public class SaveSlotButtonConfig : ScriptableObject
    {
        [field: SerializeField] public TMP_ColorGradient OnSelectColorGradient { get; private set; }
        [field: SerializeField] public TMP_ColorGradient OnUnselectColorGradient { private set; get; }

        [field: SerializeField] public OnClickScalerConfig OnClickScalerConfig { get; private set; }
    }
}