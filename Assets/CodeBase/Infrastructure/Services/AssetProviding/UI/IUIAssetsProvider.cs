using Cysharp.Threading.Tasks;
using UI.HUD;
using UI.Windows.Implementations.DeathWindow;
using UI.Windows.Implementations.MainMenu;
using UI.Windows.Implementations.PauseWindow;
using UI.Windows.Implementations.SaveSelection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.Services.AssetProviding.UI
{
    public interface IUIAssetsProvider
    {
        UniTask<Canvas> LoadCanvas();
        UniTask<EventSystem> LoadEventSystem();
        UniTask<HealthBar> LoadHealthBar();
        
        UniTask<MainMenuWindowView> LoadMainMenuWindow();
        UniTask<SaveSelectionWindowView> LoadSaveSelectionWindow();
        UniTask<PauseWindowView> LoadPauseWindow();
        UniTask<DeathWindowView> LoadDeathWindow();
    }
}