using Cinemachine;
using UnityEngine;

namespace Gameplay.Hero.PlayerCamera
{
    [CreateAssetMenu(fileName = "Player Camera Config", menuName = "Configs/Camera")]
    public class PlayerCameraConfig : ScriptableObject
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public CinemachineFreeLook Controller { get; private set; }
        [field: SerializeField] public Vector3 FollowPointOffsetFromPlayer { get; private set; }
        [field: SerializeField] public GameObject Root { get; private set; }
        [field: SerializeField] public GameObject FollowPoint { get; private set; }
    }
}