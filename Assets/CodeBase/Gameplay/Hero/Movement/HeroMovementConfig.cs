using UnityEngine;

namespace Gameplay.Hero.Movement
{
    [CreateAssetMenu(fileName = "Hero Movement Config", menuName = "Configs/Hero/Movement")]
    public class HeroMovementConfig : ScriptableObject
    {
        [Header("Ground Sphere Casting")]
        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private float _sphereRadius;
        [SerializeField] private float _distance;
        
        [Header("Rotation")] 
        [SerializeField] private float _speedInAnglesPerSec;
        
        [Header("Slope Sliding")] 
        [SerializeField] private float _slopeSlideSpeed;
        [SerializeField] private float _minSlopeAngle;

        [Header("Jog State")]
        [SerializeField] private float _jogSpeed;
        [SerializeField] private float _jogAntiBumpSpeed;
        
        [Header("Fall State")]
        [SerializeField] private float _fallVerticalSpeed;
        [SerializeField] private float _fallHorizontalSpeed;
        
        [Header("Jump State")]
        [SerializeField] private AnimationCurve _ySpeedToTime;
        [SerializeField] private float _jumpHorizontalSpeed;

        public GameObject GroundSphereCastingPointPrefab => _pointPrefab;
        public float GroundSphereCastingSphereRadius => _sphereRadius;
        public float GroundSphereCastingDistance => _distance;
        
        public float RotationSpeed => _speedInAnglesPerSec;

        public float SlopeSlideSpeed => _slopeSlideSpeed;
        public float MinSlopeAngle => _minSlopeAngle;
         
        public float JogSpeed => _jogSpeed;
        public float JogAntiBumpSpeed => _jogAntiBumpSpeed;
         
        public float FallVerticalSpeed => _fallVerticalSpeed;
        public float FallHorizontalSpeed => _fallHorizontalSpeed;
         
        public AnimationCurve JumpYSpeedToTimeCurve => _ySpeedToTime;
        public float JumpHorizontalSpeed => _jumpHorizontalSpeed;
    }
}