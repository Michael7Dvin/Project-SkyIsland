using Gameplay.Healths;
using Gameplay.Movement;
using UnityEngine;

namespace Gameplay.Heros
{
    public class HeroProgressDataProvider
    {
        private readonly Transform _transform;
        private readonly IMovement _movement;
        private readonly IHealth _health;

        public HeroProgressDataProvider(Transform transform, IMovement movement, IHealth health)
        {
            _transform = transform;
            _movement = movement;
            _health = health;
        }

        public Vector3 Position
        {
            get => _transform.position;
            set => _movement.Teleport(value);
        }

        public Quaternion Rotation
        {
            get => _transform.rotation;
            set => _transform.rotation = value;
        }

        public float Health
        {
            get => _health.Current.Value;
            set => _health.SetCurrent(value);
        }
    }
}