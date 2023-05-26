using Infrastructure.Services.Updater;
using UnityEngine;

namespace Gameplay.Movement.SlopeCalculation
{
    public class SlopeCalculator : ISlopeCalculator
    {
        private readonly IUpdater _updater;

        private readonly Transform _rayOriginPoint;
        private readonly float _rayDistance;
        private RaycastHit _slopeHit;

        public SlopeCalculator(IUpdater updater,
            Transform rayOriginPoint,
            float rayDistance)
        {
            _updater = updater;
            _rayOriginPoint = rayOriginPoint;
            _rayDistance = rayDistance;

            _updater.FixedUpdated += FixedUpdate;
        }
        
        public float SlopeAngle { get; private set; }

        public void Dispose() => 
            _updater.FixedUpdated -= FixedUpdate;

        private void FixedUpdate(float deltaTime) => 
            CalculateGetSlopeAngle();

        private void CalculateGetSlopeAngle()
        {
            Vector3 rayDirection = Vector3.down;
            
            if (Physics.Raycast(_rayOriginPoint.position, rayDirection, out _slopeHit, _rayDistance))
            {
                SlopeAngle = Vector3.Angle(_slopeHit.normal, Vector3.up);
            }
            else
            {
                SlopeAngle = 0f;
            }
        }
    }
}