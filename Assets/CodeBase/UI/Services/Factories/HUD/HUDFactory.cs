using Cysharp.Threading.Tasks;
using Gameplay.Healths;
using Infrastructure.Services.AssetProviding.Providers.UI.HUD;
using Infrastructure.Services.Instantiating;
using UI.HUD;
using UI.Services.Providing.Utilities;
using UnityEngine;

namespace UI.Services.Factories.HUD
{
    public class HUDFactory : IHUDFactory
    {
        private readonly IHUDAssetsProvider _assetsProvider; 
        private readonly IInstantiator _instantiator;
        private readonly IUiUtilitiesProvider _uiUtilitiesProvider;

        public HUDFactory(IHUDAssetsProvider assetsProvider,
            IInstantiator instantiator,
            IUiUtilitiesProvider uiUtilitiesProvider)
        {
            _assetsProvider = assetsProvider;
            _instantiator = instantiator;
            _uiUtilitiesProvider = uiUtilitiesProvider;
        }

        private Transform Canvas =>
            _uiUtilitiesProvider.Canvas.Value.transform;
        
        public async UniTask WarmUp() => 
            await _assetsProvider.LoadHealthBar();

        public async UniTask<HealthBar> CreateHealthBar(IHealth health)
        {
            HealthBar prefab = await _assetsProvider.LoadHealthBar();
            HealthBar healthBar = _instantiator.InstantiatePrefabForComponent(prefab, Canvas);
            healthBar.Construct(health);
            return healthBar;
        }
    }
}