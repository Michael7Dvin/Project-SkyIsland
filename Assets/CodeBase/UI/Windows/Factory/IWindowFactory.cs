using UI.Services.Mediating;
using UI.Windows.Base;
using UnityEngine;

namespace UI.Windows.Factory
{
    public interface IWindowFactory
    {
        void Init(IMediator mediator);
        void ResetCanvas(Canvas canvas);
        
        IWindow CreateMainMenuWindow();
        IWindow CreateSaveSelectionWindow();
        IWindow CreatePauseWindow();
        IWindow CreateDeathWindow();
    }
}