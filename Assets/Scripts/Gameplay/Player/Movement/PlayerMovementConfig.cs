using UnityEngine;

namespace Gameplay.Player.Movement
{
    [CreateAssetMenu(fileName = "Player Movement Config", menuName = "Configs/Player/Movement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField] private float _jogSpeed;
        [SerializeField] private float _fallSpeed;

        [SerializeField] private GameObject _slopeCalculatorRayCastPointPrefab;
        [SerializeField] private float _slopeCalculatorRayCastDistance;
        
        public float JogSpeed => _jogSpeed;
        public float FallSpeed => _fallSpeed;

        public GameObject SlopeCalculatorRayCastPointPrefab => _slopeCalculatorRayCastPointPrefab;
        public float SlopeCalculatorRayCastDistance => _slopeCalculatorRayCastDistance;
    }
}