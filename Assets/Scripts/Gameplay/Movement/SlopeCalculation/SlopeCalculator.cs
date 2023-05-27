using Gameplay.Movement.GroundSpherecasting;
using UnityEngine;

namespace Gameplay.Movement.SlopeCalculation
{
    public class SlopeCalculator : ISlopeCalculator
    {
        private readonly IGroundSpherecaster _groundSpherecaster;

        public SlopeCalculator(IGroundSpherecaster groundSpherecaster)
        {
            _groundSpherecaster = groundSpherecaster;
            
            _groundSpherecaster.SphereCasted += OnSphereCasted;
            _groundSpherecaster.SphereCastMissed += OnSphereCastMissed;
        }

        public Vector3 SlopeDirection { get; private set; }
        public float SlopeAngle { get; private set; }

        public void Dispose()
        {
            _groundSpherecaster.SphereCasted -= OnSphereCasted;
            _groundSpherecaster.SphereCastMissed -= OnSphereCastMissed;
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