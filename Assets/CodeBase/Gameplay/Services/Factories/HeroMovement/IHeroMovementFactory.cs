using Cysharp.Threading.Tasks;
using Gameplay.Heros.Movement;
using UnityEngine;

namespace Gameplay.Services.Factories.HeroMovement
{
    public interface IHeroMovementFactory
    {
        UniTask WarmUp();

        UniTask<IHeroMovement> Create(Transform parent,
            Animator animator,
            CharacterController characterController,
            Transform camera);
    }
}