using UI.HUD;
using UI.Windows;
using UI.Windows.Implementations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "Configs/UI")]
    public class UIConfig : ScriptableObject
    {
        [field: SerializeField] public Canvas CanvasPrefab { get; private set; }
        [field: SerializeField] public EventSystem UIInputEventSystem { get; private set; }
        
        [field: SerializeField] public MainMenuWindow MainMenuWindowPrefab { get; private set; }
        [field: SerializeField] public SaveSelectionWindow SaveSelectionWindowPrefab { get; private set; }
        [field: SerializeField] public PauseWindow PauseWindowPrefab { get; private set; }
        [field: SerializeField] public DeathWindow DeathWindowPrefab { get; private set; }
        [field: SerializeField] public HealthBarHUD HealthBarHUDPrefab { get; private set; }
    }
}