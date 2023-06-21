using Cysharp.Threading.Tasks;
using UI.Windows.Implementations.DeathWindow;
using UI.Windows.Implementations.MainMenu;
using UI.Windows.Implementations.PauseWindow;
using UI.Windows.Implementations.SaveSelection;

namespace Infrastructure.Services.AssetProviding.UI.Windows
{
    public interface IWindowsAssetsProvider
    {
        UniTask<MainMenuWindowView> LoadMainMenuWindow();
        UniTask<SaveSelectionWindowView> LoadSaveSelectionWindow();
        UniTask<PauseWindowView> LoadPauseWindow();
        UniTask<DeathWindowView> LoadDeathWindow();
    }
}