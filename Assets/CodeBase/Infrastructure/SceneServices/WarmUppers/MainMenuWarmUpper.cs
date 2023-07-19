using Cysharp.Threading.Tasks;
using UI.Services.Factories.Utilities;
using UI.Services.Factories.Window;

namespace Infrastructure.SceneServices.WarmUppers
{
    public class MainMenuWarmUpper : IWarmUpper
    {
        private readonly IUiUtilitiesFactory _uiUtilitiesFactory;
        private readonly IWindowFactory _windowFactory;

        public MainMenuWarmUpper(IUiUtilitiesFactory uiUtilitiesFactory,
            IWindowFactory windowFactory)
        {
            _uiUtilitiesFactory = uiUtilitiesFactory;
            _windowFactory = windowFactory;
        }
        
        public async UniTask WarmUp()
        {
            await _uiUtilitiesFactory.WarmUp();
            await _windowFactory.WarmUp();
        }
    }
}