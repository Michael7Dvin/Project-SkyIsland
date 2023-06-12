using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Movement.GroundSpherecasting
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