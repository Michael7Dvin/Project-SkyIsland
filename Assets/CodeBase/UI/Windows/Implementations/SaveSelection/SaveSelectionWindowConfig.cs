using UI.AnimatedElements;
using UnityEngine;

namespace UI.Windows.Implementations.SaveSelection
{
    [CreateAssetMenu(menuName = "Configs/UI/Windows/SaveSelection", fileName = "Save Selection Window Config")]
    public class SaveSelectionWindowConfig : ScriptableObject
    {
        [field: SerializeField] public AnimatedCanvasGroupConfig AnimatedCanvasGroupConfig { get; private set; }
        [field: SerializeField] public AnimatedButtonConfig AnimatedButtonConfig { get; private set; }
    }
}