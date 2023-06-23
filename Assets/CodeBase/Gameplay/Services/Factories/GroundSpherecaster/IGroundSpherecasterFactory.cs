using Cysharp.Threading.Tasks;
using Gameplay.Movement.GroundSpherecasting;
using UnityEngine;

namespace Gameplay.Services.Factories.GroundSpherecaster
{
    public interface IGroundSpherecasterFactory
    {
        UniTask WarmUp();
        UniTask<IGroundSpherecaster> Create(Transform parent,
            Vector3 sphereCastPointOffset,
            float sphereCastRadius,
            float sphereCastDistance);
    }
}