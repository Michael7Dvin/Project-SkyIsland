using Cysharp.Threading.Tasks;
using Gameplay.Movement;
using UnityEngine;

namespace Gameplay.Services.Factories.Heros.Moving
{
    public interface IHeroMovementFactory
    {
        UniTask WarmUp();

        UniTask<IMovement> Create(Transform parent,
            Animator animator,
            CharacterController characterController);
    }
}