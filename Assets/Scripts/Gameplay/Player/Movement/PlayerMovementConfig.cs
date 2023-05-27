using UnityEngine;

namespace Gameplay.Player.Movement
{
    [CreateAssetMenu(fileName = "Player Movement Config", menuName = "Configs/Player/Movement")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField] private float _jogSpeed;
        [SerializeField] private float _fallSpeed;

        [SerializeField] private GameObject _groundSphereCastingPointPrefab;
        [SerializeField] private float _groundSphereCastingSphereRadius;
        [SerializeField] private float _groundSphereCastingDistance;

        public float JogSpeed => _jogSpeed;
        public float FallSpeed => _fallSpeed;

        public GameObject GroundSphereCastingPointPrefab => _groundSphereCastingPointPrefab;
        public float GroundSphereCastingSphereRadius => _groundSphereCastingSphereRadius;
        public float GroundSphereCastingDistance => _groundSphereCastingDistance;
    }
}