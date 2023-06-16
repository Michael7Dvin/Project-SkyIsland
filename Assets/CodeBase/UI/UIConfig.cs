using UI.Windows.Implementations.SaveSelection;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Configs/UI/UI Config", fileName = "UI Config")]
    public class UIConfig : ScriptableObject
    {
        [field: SerializeField] public SaveSelectionWindowConfig SaveSelectionWindowConfig { get; private set; }
    }
}