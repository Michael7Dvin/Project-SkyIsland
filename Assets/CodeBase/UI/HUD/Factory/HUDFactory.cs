using Cysharp.Threading.Tasks;
using Gameplay.Healths;
using Infrastructure.Services.AssetProviding.UI.HUD;
using Infrastructure.Services.Instantiating;
using UnityEngine;

namespace UI.HUD.Factory
{
    public class HUDFactory : IHUDFactory
    {
        private Canvas _canvas;

        private readonly IHUDAssetsProvider _assetsProvider; 
        private readonly IInstantiator _instantiator;

        public HUDFactory(IHUDAssetsProvider assetsProvider, IInstantiator instantiator)
        {
            _assetsProvider = assetsProvider;
            _instantiator = instantiator;
        }

        public async UniTask WarmUp() => 
            await _assetsProvider.LoadHealthBar();

        public void ResetCanvas(Canvas canvas) => 
            _canvas = canvas;

        public async UniTask<HealthBar> CreateHealthBar(IHealth health)
        {
            HealthBar prefab = await _assetsProvider.LoadHealthBar();
            HealthBar healthBar = _instantiator.Instantiate(prefab, _canvas.transform);
            healthBar.Construct(health);
            return healthBar;
        }
    }
}