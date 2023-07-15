using Gameplay.Movement.GroundSpherecasting;
using UnityEngine;

namespace Gameplay.Movement.SlopeCalculation
{
    public class SlopeCalculator : ISlopeCalculator
    {
        private readonly IGroundSphereCaster _groundSphereCaster;

        public SlopeCalculator(IGroundSphereCaster groundSphereCaster)
        {
            _groundSphereCaster = groundSphereCaster;
            
            _groundSphereCaster.SphereCasted += OnSphereCasted;
            _groundSphereCaster.SphereCastMissed += OnSphereCastMissed;
        }

        public Vector3 SlopeDirection { get; private set; }
        public float SlopeAngle { get; private set; }

        public void Dispose()
        {
            _groundSphereCaster.SphereCasted -= OnSphereCasted;
            _groundSphereCaster.SphereCastMissed -= OnSphereCastMissed;
        }

        private void OnSphereCasted(RaycastHit hit)
        {
            Vector3 surfaceNormal = hit.normal;
            
            SlopeAngle = Vector3.Angle(surfaceNormal, Vector3.up);
            SlopeDirection = Vector3.up - surfaceNormal * Vector3.Dot(Vector3.up, surfaceNormal);
        }

        private void OnSphereCastMissed()
        {
            SlopeAngle = 0f;
            SlopeDirection = Vector3.zero;
        }
    }
}