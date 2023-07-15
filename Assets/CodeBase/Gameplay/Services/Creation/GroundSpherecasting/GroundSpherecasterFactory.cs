using Cysharp.Threading.Tasks;
using Gameplay.Movement.GroundSpherecasting;
using Infrastructure.Services.AssetProviding.Providers.Common;
using Infrastructure.Services.Instantiating;
using UnityEngine;

namespace Gameplay.Services.Creation.GroundSpherecasting
{
    public class GroundSpherecasterFactory : IGroundSpherecasterFactory
    {
        private const string SpherecastPointName = "Spherecast Point";
        
        private readonly ICommonAssetsProvider _commonAssetsProvider;
        private readonly IInstantiator _instantiator;

        public GroundSpherecasterFactory(ICommonAssetsProvider commonAssetsProvider,
            IInstantiator instantiator)
        {
            _commonAssetsProvider = commonAssetsProvider;
            _instantiator = instantiator;
        }

        public async UniTask WarmUp() => 
            await _commonAssetsProvider.LoadEmptyGameObject();

        public async UniTask<IGroundSphereCaster> Create(Transform parent,
            Vector3 sphereCastPointOffset,
            float sphereCastRadius,
            float sphereCastDistance)
        {
            GameObject prefab = await _commonAssetsProvider.LoadEmptyGameObject();
            
            GameObject spherecastPoint = _instantiator.InstantiatePrefab(prefab, parent);
            spherecastPoint.name = SpherecastPointName;
            spherecastPoint.transform.localPosition = sphereCastPointOffset;

            GroundSphereCaster groundSphereCaster = _instantiator.Instantiate<GroundSphereCaster>();
            groundSphereCaster.Construct(spherecastPoint.transform, sphereCastRadius, sphereCastDistance);

            return groundSphereCaster;
        }
    }
}