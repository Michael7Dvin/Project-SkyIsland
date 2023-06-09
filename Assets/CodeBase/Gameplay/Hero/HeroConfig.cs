﻿using Gameplay.Hero.Movement;
using Gameplay.Hero.PlayerCamera;
using Gameplay.MonoBehaviours;
using UnityEngine;

namespace Gameplay.Hero
{
    [CreateAssetMenu(fileName = "Hero Config", menuName = "Configs/Hero/Config")]
    public class HeroConfig : ScriptableObject
    {
        [field: SerializeField] public GameObjectLifeCycleNotifier HeroPrefab { get; private set; }
        [field: SerializeField] public HeroMovementConfig Movement { get; private set; }
        [field: SerializeField] public HeroCameraConfig Camera { get; private set; }
        [field: SerializeField] public HeroStatsConfig Stats { get; private set; }
    }
}