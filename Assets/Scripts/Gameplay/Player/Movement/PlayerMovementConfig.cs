using UnityEngine;

namespace Gameplay.Player.Movement
{
    [CreateAssetMenu(fileName = "Player Movement Config", menuName = "Configs/Player/Movement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField] private float _jogSpeed;
        [SerializeField] private float _fallSpeed;

        [SerializeField] private GameObject _slopeCalculatorSphereCastPointPrefab;
        [SerializeField] private float _slopeCalculatorSphereCastRadius;
        [SerializeField] private float _slopeCalculatorSphereCastDistance;

        public float JogSpeed => _jogSpeed;
        public float FallSpeed => _fallSpeed;

        public GameObject SlopeCalculatorSphereCastPointPrefab => _slopeCalculatorSphereCastPointPrefab;
        public float SlopeCalculatorSphereCastRadius => _slopeCalculatorSphereCastRadius;
        public float SlopeCalculatorSphereCastDistance => _slopeCalculatorSphereCastDistance;
    }
}