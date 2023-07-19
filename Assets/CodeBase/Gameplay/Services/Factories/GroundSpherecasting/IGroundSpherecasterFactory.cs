using Cysharp.Threading.Tasks;
using Gameplay.Movement.GroundSpherecasting;
using UnityEngine;

namespace Gameplay.Services.Factories.GroundSpherecasting
{
    public interface IGroundSpherecasterFactory
    {
        UniTask WarmUp();
        UniTask<GroundSphereCaster> Create(Transform parent,
            Vector3 sphereCastPointOffset,
            float sphereCastRadius,
            float sphereCastDistance);
    }
}