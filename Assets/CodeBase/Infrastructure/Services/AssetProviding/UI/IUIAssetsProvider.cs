using Cysharp.Threading.Tasks;
using UI.HUD;
using UI.Windows.Implementations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Infrastructure.Services.AssetProviding.UI
{
    public interface IUIAssetsProvider
    {
        UniTask<Canvas> LoadCanvas();
        UniTask<EventSystem> LoadEventSystem();
        UniTask<HealthBar> LoadHealthBar();
        
        UniTask<MainMenuWindow> LoadMainMenuWindow();
        UniTask<SaveSelectionWindow> LoadSaveSelectionWindow();
        UniTask<PauseWindow> LoadPauseWindow();
        UniTask<DeathWindow> LoadDeathWindow();
    }
}