using TMPro;
using TweenAnimations;
using UnityEngine;

namespace UI.Elements.Buttons.SaveSlot
{
    [CreateAssetMenu(menuName = "Configs/UI/Elements/Save Slot Button", fileName = "Save Slot Button")]
    public class SaveSlotButtonConfig : ScriptableObject
    {
        [field: SerializeField] public string OnSelectedText { get; private set; }
        [field: SerializeField] public string OnUnelectedText { get; private set; }
        [field: SerializeField] public TMP_ColorGradient SelectedColorGradient { get; private set; }
        [field: SerializeField] public TMP_ColorGradient UnselectedColorGradient { get; private set; }
        [field: SerializeField] public ScalingConfig OnButtonDownScalingConfig { get; private set; }
        [field: SerializeField] public ScalingConfig OnButtonUpScalingConfig { get; private set; }
    }
}