using UnityEngine;

namespace Gameplay.Movement.Rotator
{
    public interface IRotator
    {
        Quaternion GetRotationToDirection(Vector3 direction, Quaternion currentRotation, float deltaTime);
    }
}