using Cysharp.Threading.Tasks;
using UI.Services.Mediating;
using UI.Windows.Implementations;
using UnityEngine;

namespace UI.Windows.Factory
{
    public interface IWindowFactory
    {
        void Init(IMediator mediator);
        UniTask WarmUp();
        
        void ResetCanvas(Canvas canvas);
        
        UniTask<MainMenuWindow> CreateMainMenuWindow();
        UniTask<SaveSelectionWindow> CreateSaveSelectionWindow();
        UniTask<PauseWindow> CreatePauseWindow();
        UniTask<DeathWindow> CreateDeathWindow();
    }
}