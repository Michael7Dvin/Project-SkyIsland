using UI.Animators.WindowMover;
using UI.Animators.WindowScaler;
using UI.Controls.Buttons.Close;
using UI.Controls.Buttons.Selectable;
using UnityEngine;

namespace UI.Windows.Implementations.PauseWindow
{
    [CreateAssetMenu(menuName = "Configs/UI/Windows/Pause", fileName = "Pause")]
    public class PauseWindowConfig : ScriptableObject
    {
        [field: SerializeField] public WindowScalerConfig WindowScalerConfig { get; private set; }
        [field: SerializeField] public WindowMoverConfig WindowMoverConfig { get; private set; }
        [field: SerializeField] public CloseButtonConfig CloseButtonConfig { get; private set; }
        [field: SerializeField] public SelectableButtonConfig OptionsButtonConfig { get; private set; }
        [field: SerializeField] public SelectableButtonConfig SaveButtonConfig { get; private set; }
        [field: SerializeField] public SelectableButtonConfig MainMenuButtonConfig { get; private set; }
    }
}