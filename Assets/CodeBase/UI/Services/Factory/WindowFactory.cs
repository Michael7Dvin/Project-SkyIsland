using Infrastructure.GameFSM;
using Infrastructure.Services.AppClosing;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.StaticDataProviding;
using UI.Windows;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Services.Factory
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly UIConfig _config;
        
        private readonly IAppCloser _appCloser;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataProvider _staticDataProvider;
        
        private Canvas _canvas;
        private EventSystem _uiInputEventSystem;
        
        public WindowFactory(IInstantiator instantiator,
            IAppCloser appCloser,
            IGameStateMachine gameStateMachine,
            IStaticDataProvider staticDataProvider)
        {
            _instantiator = instantiator;
            _appCloser = appCloser;
            _gameStateMachine = gameStateMachine;
            _staticDataProvider = staticDataProvider;

            _config = staticDataProvider.GetUIConfig();
        }

        public void CreateMainMenuWindow()
        {
            CreateCanvasIfNotExist();
            CreateUIInputEventSystemIfNotExist();
            
            MainMenuWindow mainMenuWindow = _instantiator.Instantiate(_config.MainMenuWindowPrefab, _canvas.transform);
            mainMenuWindow.Construct(this, _appCloser);
        }

        public void CreateSaveSelectionWindow()
        {
            CreateCanvasIfNotExist();
            CreateUIInputEventSystemIfNotExist();

            SaveSelectionWindow saveSelectionWindow = _instantiator.Instantiate(_config.SaveSelectionWindowPrefab, _canvas.transform);
            saveSelectionWindow.Construct(_gameStateMachine, _staticDataProvider);
        }

        public void CreatePauseWindow()
        {
            CreateCanvasIfNotExist();
            CreateUIInputEventSystemIfNotExist();

            PauseWindow pauseWindow = _instantiator.Instantiate(_config.PauseWindowPrefab, _canvas.transform);
            pauseWindow.Construct(_gameStateMachine);
        }

        public void CreateDeathWindow()
        {
            CreateCanvasIfNotExist();
            CreateUIInputEventSystemIfNotExist();

            DeathWindow deathWindow = _instantiator.Instantiate(_config.DeathWindowPrefab, _canvas.transform);
            deathWindow.Construct(_gameStateMachine);
        }

        private void CreateCanvasIfNotExist()
        {
            if (_canvas == null)
            {
                CreateCanvas();
            }
        }

        private void CreateCanvas()
        {
            Canvas canvas = _instantiator.Instantiate(_config.CanvasPrefab);
            _canvas = canvas;
        }

        private void CreateUIInputEventSystemIfNotExist()
        {
            if (_uiInputEventSystem == null)
            {
                CreateUIInputEventSystem();
            }
        }

        private void CreateUIInputEventSystem()
        {
            EventSystem eventSystem = _instantiator.Instantiate(_config.UIInputEventSystem);
            _uiInputEventSystem = eventSystem;
        }
    }
}