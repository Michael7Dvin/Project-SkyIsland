using Cinemachine;
using UnityEngine;

namespace Gameplay.Player.PlayerCamera
{
    [CreateAssetMenu(fileName = "Player Camera Config", menuName = "Configs/Player/Camera")]
    public class PlayerCameraConfig : ScriptableObject
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public CinemachineFreeLook Controller { get; private set; }
        [field: SerializeField] public Vector3 FollowPointOffsetFromPlayer { get; private set; }
        [field: SerializeField] public GameObject Root { get; private set; }
        [field: SerializeField] public GameObject FollowPoint { get; private set; }
    }
}