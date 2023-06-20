using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.UI;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.Mediating;
using UI.Windows.Base.Window;
using UI.Windows.Implementations.DeathWindow;
using UI.Windows.Implementations.MainMenu;
using UI.Windows.Implementations.PauseWindow;
using UI.Windows.Implementations.SaveSelection;
using UnityEngine;

namespace UI.Windows.Factory
{
    public class WindowFactory : IWindowFactory
    {
        private Canvas _canvas;

        private readonly AllUIConfigs _allUIConfigs;
        private readonly IUIAssetsProvider _uiAssetsProvider;
        private readonly IInstantiator _instantiator;
        private readonly ICustomLogger _logger;

        private IMediator _mediator;

        public WindowFactory(IStaticDataProvider staticDataProvider,
            IUIAssetsProvider uiAssetsProvider,
            IInstantiator instantiator,
            ICustomLogger logger)
        {
            _allUIConfigs = staticDataProvider.AllUIConfigs;
            _uiAssetsProvider = uiAssetsProvider;
            _instantiator = instantiator;
            _logger = logger;
        }
        
        public void Init(IMediator mediator) => 
            _mediator = mediator;

        public async UniTask WarmUp()
        {
            await _uiAssetsProvider.LoadMainMenuWindow();
            await _uiAssetsProvider.LoadSaveSelectionWindow();
            await _uiAssetsProvider.LoadPauseWindow();
            await _uiAssetsProvider.LoadDeathWindow();
        }

        public void ResetCanvas(Canvas canvas) => 
            _canvas = canvas;

        public async UniTask<IWindow> Create(WindowType type)
        {
            IWindow window;
            switch (type)
            {
                case WindowType.MainMenu:
                    window = await CreateMainMenu();
                    break;
                case WindowType.SaveSelection:
                    window = await CreateSaveSelection();
                    break;
                case WindowType.Pause:
                    window = await CreatePause();
                    break;
                case WindowType.Death:
                    window = await CreateDeath();
                    break;
                default:
                    _logger.LogError($"Unsupported {nameof(WindowType)}: '{type}'");
                    return null;
            }

            return window;
        }
        
        private async UniTask<MainMenuWindow> CreateMainMenu()
        {
            MainMenuWindowView viewPrefab = await _uiAssetsProvider.LoadMainMenuWindow();

            MainMenuWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            view.Construct(_allUIConfigs.MainMenu);
            
            MainMenuWindowLogic logic = new(_mediator);
            MainMenuWindow window = new(view, logic);

            return window;
        }

        private async UniTask<SaveSelectionWindow> CreateSaveSelection()
        {
            SaveSelectionWindowView viewPrefab = await _uiAssetsProvider.LoadSaveSelectionWindow();
            
            SaveSelectionWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            view.Construct(_allUIConfigs.SaveSelection);
            
            SaveSelectionWindowLogic logic = new(_mediator);
            SaveSelectionWindow window = new(view, logic);

            return window;
        }

        private async UniTask<PauseWindow> CreatePause()
        {
            PauseWindowView viewPrefab = await _uiAssetsProvider.LoadPauseWindow();
            
            PauseWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            view.Construct(_allUIConfigs.Pause);
            
            PauseWindowLogic logic = new(_mediator);
            PauseWindow window = new(view, logic);

            return window;
        }

        private async UniTask<DeathWindow> CreateDeath()
        {
            DeathWindowView viewPrefab = await _uiAssetsProvider.LoadDeathWindow();
            
            DeathWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            view.Construct(_allUIConfigs.Death);
            
            DeathWindowLogic logic = new(_mediator);
            DeathWindow window = new(view, logic);

            return window;
        }
    }
}