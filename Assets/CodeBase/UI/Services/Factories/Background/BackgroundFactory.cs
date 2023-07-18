using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Providers.UI.Backgrounds;
using UI.Services.Providing.Utilities;
using UnityEngine;
using Zenject;

namespace UI.Services.Factories.Background
{
    public class BackgroundFactory : IBackgroundFactory
    {
        private readonly IBackgroundsAssetsProvider _assetsProvider;
        private readonly IInstantiator _instantiator;
        private readonly IUiUtilitiesProvider _uiUtilitiesProvider;

        public BackgroundFactory(IBackgroundsAssetsProvider assetsProvider,
            IInstantiator instantiator,
            IUiUtilitiesProvider uiUtilitiesProvider)
        {
            _assetsProvider = assetsProvider;
            _instantiator = instantiator;
            _uiUtilitiesProvider = uiUtilitiesProvider;
        }

        private Transform Canvas =>
            _uiUtilitiesProvider.Canvas.Value.transform;

        public async UniTask WarmUp()
        {
            await _assetsProvider.LoadMainMenu();
            await _assetsProvider.LoadPause();
            await _assetsProvider.LoadDeath();
        }
        
        public async UniTask<GameObject> CreateMainMenu()
        {
            GameObject prefab = await _assetsProvider.LoadMainMenu();
            GameObject background = _instantiator.InstantiatePrefab(prefab, Canvas);
            MakeFirstInCanvasHierarchy(background);
            return background;
        }

        public async UniTask<GameObject> CreatePause()
        {
            GameObject prefab = await _assetsProvider.LoadPause();
            GameObject background = _instantiator.InstantiatePrefab(prefab, Canvas);
            MakeFirstInCanvasHierarchy(background);
            return background;
        }

        public async UniTask<GameObject> CreateDeath()
        {
            GameObject prefab = await _assetsProvider.LoadDeath();
            GameObject background = _instantiator.InstantiatePrefab(prefab, Canvas);
            MakeFirstInCanvasHierarchy(background);
            return background;
        }
        
        private void MakeFirstInCanvasHierarchy(GameObject background) => 
            background.transform.SetSiblingIndex(0);
    }
}