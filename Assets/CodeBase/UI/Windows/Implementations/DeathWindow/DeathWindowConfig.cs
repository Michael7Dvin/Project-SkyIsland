using UI.Elements.Buttons.Selectable;
using UnityEngine;

namespace UI.Windows.Implementations.DeathWindow
{
    [CreateAssetMenu(menuName = "Configs/UI/Windows/Death", fileName = "Death")]
    public class DeathWindowConfig : ScriptableObject
    {
        [field: SerializeField] public SelectableButtonConfig RespawnButtonConfig { get; private set; }
        [field: SerializeField] public SelectableButtonConfig MainMenuButtonConfig { get; private set; }
    }
}