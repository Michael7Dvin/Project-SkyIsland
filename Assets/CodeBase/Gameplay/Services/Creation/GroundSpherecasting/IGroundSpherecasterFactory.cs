using Cysharp.Threading.Tasks;
using Gameplay.Movement.GroundSpherecasting;
using UnityEngine;

namespace Gameplay.Services.Creation.GroundSpherecasting
{
    public interface IGroundSpherecasterFactory
    {
        UniTask WarmUp();
        UniTask<IGroundSphereCaster> Create(Transform parent,
            Vector3 sphereCastPointOffset,
            float sphereCastRadius,
            float sphereCastDistance);
    }
}