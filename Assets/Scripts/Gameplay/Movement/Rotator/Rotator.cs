using UnityEngine;

namespace Gameplay.Movement.Rotator
{
    public class Rotator : IRotator
    {
        private readonly float _speed;

        public Rotator(float speed)
        {
            _speed = speed;
        }
        
        public Quaternion GetRotationToDirection(Vector3 direction, Quaternion currentRotation, float deltaTime)
        {
            if (direction.x == 0f && direction.z == 0f)
                return currentRotation;

            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
            rotation = Quaternion.RotateTowards(currentRotation, rotation, _speed * deltaTime);
            return rotation;
        }
    }
}