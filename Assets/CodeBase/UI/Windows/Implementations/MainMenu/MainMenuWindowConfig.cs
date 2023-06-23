using UI.Controls.Buttons.Selectable;
using UnityEngine;

namespace UI.Windows.Implementations.MainMenu
{
    [CreateAssetMenu(menuName = "Configs/UI/Windows/Main Menu", fileName = "Main Menu")]
    public class MainMenuWindowConfig : ScriptableObject
    {
        [field: SerializeField] public SelectableButtonConfig PlayButtonConfig { get; private set; }
        [field: SerializeField] public SelectableButtonConfig OptionsButtonConfig { get; private set; }
        [field: SerializeField] public SelectableButtonConfig QuitButtonConfig { get; private set; }
    }
}