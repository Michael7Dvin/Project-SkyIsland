using Infrastructure.Services.Instantiating;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.Mediating;
using UI.Windows.Base;
using UI.Windows.Implementations;
using UnityEngine;

namespace UI.Windows.Factory
{
    public class WindowFactory : IWindowFactory
    {
        private Canvas _canvas;
        
        private readonly UIConfig _config;
        
        private readonly IInstantiator _instantiator;
        private IMediator _mediator;

        public WindowFactory(IInstantiator instantiator, IStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _config = staticDataProvider.GetUIConfig();
        }

        public void Init(IMediator mediator) => 
            _mediator = mediator;

        public void ResetCanvas(Canvas canvas) => 
            _canvas = canvas;

        public IWindow CreateMainMenuWindow()
        {
            Debug.Log(_canvas);
            
            MainMenuWindow mainMenuWindow = _instantiator.Instantiate(_config.MainMenuWindowPrefab, _canvas.transform);
            mainMenuWindow.Construct(_mediator);
            
            return mainMenuWindow;
        }

        public IWindow CreateSaveSelectionWindow()
        {
            SaveSelectionWindow saveSelectionWindow = _instantiator.Instantiate(_config.SaveSelectionWindowPrefab, _canvas.transform);
            saveSelectionWindow.Construct(_mediator);

            return saveSelectionWindow;
        }

        public IWindow CreatePauseWindow()
        {
            PauseWindow pauseWindow = _instantiator.Instantiate(_config.PauseWindowPrefab, _canvas.transform);
            pauseWindow.Construct(_mediator);

            return pauseWindow;
        }

        public IWindow CreateDeathWindow()
        {
            DeathWindow deathWindow = _instantiator.Instantiate(_config.DeathWindowPrefab, _canvas.transform);
            deathWindow.Construct(_mediator);

            return deathWindow;
        }
    }
}