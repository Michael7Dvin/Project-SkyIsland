using Gameplay.Services.Factories.HeroMovement;
using UnityEngine;

namespace Gameplay.Services.Factories.HeroFactory
{
    [CreateAssetMenu(fileName = "Hero Config", menuName = "Configs/Hero/Config")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public HeroMovementConfig Movement { get; private set; }
        [field: SerializeField] public HeroStatsConfig Stats { get; private set; }
    }
}