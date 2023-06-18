using UI.Animators.WindowMover;
using UI.Animators.WindowScaler;
using UI.Elements.Buttons.Close;
using UI.Elements.Buttons.SaveSlot;
using UnityEngine;

namespace UI.Windows.Implementations.SaveSelection
{
    [CreateAssetMenu(menuName = "Configs/UI/Windows/Save Selection", fileName = "Save Selection")]
    public class SaveSelectionWindowConfig : ScriptableObject
    {
        [field: SerializeField] public WindowScalerConfig WindowScalerConfig { get; private set; }
        [field: SerializeField] public WindowMoverConfig WindowMoverConfig { get; private set; }
        [field: SerializeField] public CloseButtonConfig CloseButtonConfig { get; private set; }
        [field: SerializeField] public SaveSlotButtonConfig SaveSlotButtonConfig { get; private set; }
    }
}