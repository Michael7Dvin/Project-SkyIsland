using UI.Elements.Animated;
using UnityEngine;

namespace UI.Windows.Implementations.SaveSelection
{
    [CreateAssetMenu(menuName = "Configs/UI/Windows/SaveSelection", fileName = "Save Selection Window Config")]
    public class SaveSelectionWindowConfig : ScriptableObject
    {
        [field: SerializeField] public AnimatedCanvasGroupConfig AnimatedCanvasGroupConfig { get; private set; }
        [field: SerializeField] public AnimatedButtonConfig CloseButtonConfig { get; private set; }
        [field: SerializeField] public AnimatedButtonConfig SaveSlotButtonConfig { get; private set; }
    }
}