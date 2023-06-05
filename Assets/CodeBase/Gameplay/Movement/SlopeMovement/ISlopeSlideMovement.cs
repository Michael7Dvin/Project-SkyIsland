using UnityEngine;

namespace Gameplay.Movement.SlopeMovement
{
    public interface ISlopeSlideMovement
    {
        bool IsSteepSlope { get; }
        Vector3 GetSlideDownSlopeVelocity(float deltaTime);
    }
}