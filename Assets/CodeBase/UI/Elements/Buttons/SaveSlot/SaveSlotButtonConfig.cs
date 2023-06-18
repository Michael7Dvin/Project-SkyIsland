using TMPro;
using UI.Animators.OnClickScaler;
using UnityEngine;

namespace UI.Elements.Buttons.SaveSlot
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Save Slot Button", fileName = "Save Slot Button")]
    public class SaveSlotButtonConfig : ScriptableObject
    {
        [field: SerializeField] public string OnSelectedText { get; private set; }
        [field: SerializeField] public string OnUnelectedText { get; private set; }
        [field: SerializeField] public OnClickScalerConfig OnClickScalerConfig { get; private set; }
        [field: SerializeField] public TMP_ColorGradient SelectedColorGradient { get; private set; }
        [field: SerializeField] public TMP_ColorGradient UnselectedColorGradient { get; private set; }
    }
}