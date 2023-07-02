using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Providers.UI.Windows;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using UI.Services.Factories.Background;
using UI.Services.Mediating;
using UI.Windows;
using UI.Windows.Base.Window;
using UI.Windows.Implementations.DeathWindow;
using UI.Windows.Implementations.MainMenu;
using UI.Windows.Implementations.PauseWindow;
using UI.Windows.Implementations.SaveSelection;
using UnityEngine;

namespace UI.Services.Factories.Window
{
    public class WindowFactory : IWindowFactory
    {
        private Canvas _canvas;

        private readonly WindowsConfigs _windowsConfigs;
        private readonly IWindowsAssetsProvider _assetsProvider;
        private readonly ICustomLogger _logger;
        private readonly IBackgroundFactory _backgroundFactory; 
        private readonly IInstantiator _instantiator;

        private IMediator _mediator;

        public WindowFactory(IStaticDataProvider staticDataProvider,
            IWindowsAssetsProvider assetsProvider,
            ICustomLogger logger,
            IBackgroundFactory backgroundFactory,
            IInstantiator instantiator)
        {
            _windowsConfigs = staticDataProvider.WindowsConfigs;
            _assetsProvider = assetsProvider;
            _logger = logger;
            _backgroundFactory = backgroundFactory;
            _instantiator = instantiator;
        }

        public void Init(IMediator mediator) => 
            _mediator = mediator;

        public async UniTask WarmUp()
        {
            await _backgroundFactory.WarmUp();
            
            await _assetsProvider.LoadMainMenuWindow();
            await _assetsProvider.LoadSaveSelectionWindow();
            await _assetsProvider.LoadPauseWindow();
            await _assetsProvider.LoadDeathWindow();
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
            MainMenuWindowView viewPrefab = await _assetsProvider.LoadMainMenuWindow();

            MainMenuWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            GameObject background = await _backgroundFactory.CreateMainMenu();
            view.Construct(_windowsConfigs.MainMenu, background);
            
            MainMenuWindowLogic logic = new(_mediator);
            MainMenuWindow window = new(view, logic);

            return window;
        }

        private async UniTask<SaveSelectionWindow> CreateSaveSelection()
        {
            SaveSelectionWindowView viewPrefab = await _assetsProvider.LoadSaveSelectionWindow();
            
            SaveSelectionWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            view.Construct(_windowsConfigs.SaveSelection);
            
            SaveSelectionWindowLogic logic = new(_mediator);
            SaveSelectionWindow window = new(view, logic);
            
            return window;
        }

        private async UniTask<PauseWindow> CreatePause()
        {
            PauseWindowView viewPrefab = await _assetsProvider.LoadPauseWindow();
            
            PauseWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            GameObject background = await _backgroundFactory.CreatePause();
            view.Construct(_windowsConfigs.Pause, background);
            
            PauseWindowLogic logic = new(_mediator);
            PauseWindow window = new(view, logic);

            return window;
        }

        private async UniTask<DeathWindow> CreateDeath()
        {
            DeathWindowView viewPrefab = await _assetsProvider.LoadDeathWindow();
            
            DeathWindowView view = _instantiator.Instantiate(viewPrefab, _canvas.transform);
            GameObject background = await _backgroundFactory.CreateDeath();
            view.Construct(_windowsConfigs.Death, background);
            
            DeathWindowLogic logic = new(_mediator);
            DeathWindow window = new(view, logic);

            return window;
        }
    }
}