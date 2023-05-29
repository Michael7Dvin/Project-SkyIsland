using UnityEngine;

namespace Gameplay.Player.Movement
{
    [CreateAssetMenu(fileName = "Player Movement Config", menuName = "Configs/Player/Movement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [Header("Jog State")]
        [SerializeField] private float _jogSpeed;
        [SerializeField] private float _jogAntiBumpSpeed;
        
        [Header("Fall State")]
        [SerializeField] private float _fallVerticalSpeed;
        [SerializeField] private float _fallHorizontalSpeed;
        
        [Header("Jump State")]
        [SerializeField] private AnimationCurve _ySpeedToTime;
        [SerializeField] private float _jumpHorizontalSpeed;

        [Header("Slope Sliding")] 
        [SerializeField] private float _slopeSlideSpeed;
        [SerializeField] private float _minSlopeAngle;

        [Header("Ground Sphere Casting")]
        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private float _sphereRadius;
        [SerializeField] private float _distance;
        
         public float JogSpeed => _jogSpeed;
         public float JogAntiBumpSpeed => _jogAntiBumpSpeed;
         
         public float FallVerticalSpeed => _fallVerticalSpeed;
         public float FallHorizontalSpeed => _fallHorizontalSpeed;
         
         public AnimationCurve JumpYSpeedToTimeCurve => _ySpeedToTime;
         public float JumpHorizontalSpeed => _jumpHorizontalSpeed;

         public float SlopeSlideSpeed => _slopeSlideSpeed;
         public float MinSlopeAngle => _minSlopeAngle;

         public GameObject GroundSphereCastingPointPrefab => _pointPrefab;
         public float GroundSphereCastingSphereRadius => _sphereRadius;
         public float GroundSphereCastingDistance => _distance;
    }
}