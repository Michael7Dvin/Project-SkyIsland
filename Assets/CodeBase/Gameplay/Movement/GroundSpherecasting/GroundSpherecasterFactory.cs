using Cysharp.Threading.Tasks;
using Infrastructure.Services.AssetProviding.Common;
using Infrastructure.Services.Instantiating;
using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.GroundSpherecasting
{
    public class GroundSpherecasterFactory : IGroundSpherecasterFactory
    {
        private const string SpherecastPointName = "Spherecast Point";
        
        private readonly ICommonAssetsProvider _commonAssetsProvider;
        private readonly IInstantiator _instantiator;
        private readonly IUpdater _updater;
        
        public GroundSpherecasterFactory(ICommonAssetsProvider commonAssetsProvider,
            IInstantiator instantiator,
            IUpdater updater)
        {
            _commonAssetsProvider = commonAssetsProvider;
            _instantiator = instantiator;
            _updater = updater;
        }

        public async UniTask WarmUp() => 
            await _commonAssetsProvider.LoadEmptyGameObject();

        public async UniTask<IGroundSpherecaster> Create(Transform parent,
            Vector3 sphereCastPointOffset,
            float sphereCastRadius,
            float sphereCastDistance)
        {
            GameObject prefab = await _commonAssetsProvider.LoadEmptyGameObject();
            
            GameObject spherecastPoint = _instantiator.Instantiate(prefab, parent);
            spherecastPoint.name = SpherecastPointName;
            spherecastPoint.transform.localPosition = sphereCastPointOffset;
            
            return new GroundSpherecaster(_updater, spherecastPoint.transform, sphereCastRadius, sphereCastDistance);
        }
    }
}