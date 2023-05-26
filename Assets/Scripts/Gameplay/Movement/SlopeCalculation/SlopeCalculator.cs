using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.SlopeCalculation
{
    public class SlopeCalculator : ISlopeCalculator
    {
        private readonly IUpdater _updater;

        private readonly Transform _rayOriginPoint;
        private readonly float _sphereCastRadius;
        private readonly float _sphereCastDistance;

        public SlopeCalculator(IUpdater updater,
            Transform rayOriginPoint,
            float sphereCastRadius,
            float sphereCastDistance)
        {
            _updater = updater;
            _rayOriginPoint = rayOriginPoint;
            _sphereCastDistance = sphereCastDistance;
            _sphereCastRadius = sphereCastRadius;

            _updater.FixedUpdated += FixedUpdate;
        }
        
        public Vector3 SlopeDirection { get; private set; }
        public float SlopeAngle { get; private set; }

        public void Dispose() => 
            _updater.FixedUpdated -= FixedUpdate;

        private void FixedUpdate(float deltaTime) => 
            CalculateGetSlopeAngle();

        private void CalculateGetSlopeAngle()
        {
            if (Physics.SphereCast(_rayOriginPoint.position, _sphereCastRadius, Vector3.down, out RaycastHit hit, _sphereCastDistance))
            {
                Vector3 surfaceNormal = hit.normal;
                SlopeAngle = Vector3.Angle(surfaceNormal, Vector3.up);
                SlopeDirection = Vector3.up - surfaceNormal * Vector3.Dot(Vector3.up, surfaceNormal);
            }
            else
            {
                SlopeAngle = 0f;
                SlopeDirection = Vector3.zero;
            }
        }
    }
}