using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.UI.Backgrounds;
using Infrastructure.Services.Instantiating;
using UnityEngine;

namespace UI.Services.BackgroundFactory
{
    public class BackgroundFactory : IBackgroundFactory
    {
        private Canvas _canvas;

        private readonly IBackgroundsAssetsProvider _assetsProvider;

        public BackgroundFactory(IBackgroundsAssetsProvider assetsProvider, IInstantiator instantiator)
        {
            _assetsProvider = assetsProvider;
            _instantiator = instantiator;
        }

        private readonly IInstantiator _instantiator;

        public async UniTask WarmUp()
        {
            await _assetsProvider.LoadMainMenu();
            await _assetsProvider.LoadPause();
            await _assetsProvider.LoadDeath();
        }

        public void ResetCanvas(Canvas canvas) => 
            _canvas = canvas;

        public async UniTask<GameObject> CreateMainMenu()
        {
            GameObject prefab = await _assetsProvider.LoadMainMenu();
            GameObject background = _instantiator.Instantiate(prefab, _canvas.transform);
            MakeFirstInCanvasHierarchy(background);
            return background;
        }

        public async UniTask<GameObject> CreatePause()
        {
            GameObject prefab = await _assetsProvider.LoadPause();
            GameObject background = _instantiator.Instantiate(prefab, _canvas.transform);
            MakeFirstInCanvasHierarchy(background);
            return background;
        }

        public async UniTask<GameObject> CreateDeath()
        {
            GameObject prefab = await _assetsProvider.LoadDeath();
            GameObject background = _instantiator.Instantiate(prefab, _canvas.transform);
            MakeFirstInCanvasHierarchy(background);
            return background;
        }
        
        private void MakeFirstInCanvasHierarchy(GameObject background) => 
            background.transform.SetSiblingIndex(0);
    }
}