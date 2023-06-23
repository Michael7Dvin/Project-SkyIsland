using UI.Windows.Implementations.DeathWindow;
using UI.Windows.Implementations.MainMenu;
using UI.Windows.Implementations.PauseWindow;
using UI.Windows.Implementations.SaveSelection;
using UnityEngine;

namespace UI.Services.Factories.Window
{
    [CreateAssetMenu(menuName = "Configs/UI/All UI", fileName = "All UI")]
    public class WindowsConfigs : ScriptableObject
    {
        [field: SerializeField] public SaveSelectionWindowConfig SaveSelection { get; private set; }
        [field: SerializeField] public MainMenuWindowConfig MainMenu { get; private set; }
        [field: SerializeField] public PauseWindowConfig Pause { get; private set; }
        [field: SerializeField] public DeathWindowConfig Death { get; private set; }
    }
}