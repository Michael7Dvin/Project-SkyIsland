using Gameplay.Services.Factories.Heros.Moving;
using UnityEngine;

namespace Gameplay.Services.Factories.Heros
{
    [CreateAssetMenu(fileName = "Hero Config", menuName = "Configs/Hero/Config")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 CameraFollowPointOffset { get; private set; }
        [field: SerializeField] public HeroMovementConfig Movement { get; private set; }
        [field: SerializeField] public HeroStatsConfig Stats { get; private set; }
    }
}