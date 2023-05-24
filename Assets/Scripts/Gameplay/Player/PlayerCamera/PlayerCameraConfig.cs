using Cinemachine;
using UnityEngine;

namespace Gameplay.Player.PlayerCamera
{
    [CreateAssetMenu(fileName = "Player Camera Config", menuName = "Configs/Player/Camera")]
    public class PlayerCameraConfig : ScriptableObject
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineFreeLook _controller;
        [SerializeField] private Vector3 _followPointOffsetFromPlayer;
        [SerializeField] private GameObject _emptyCameraParent;
        [SerializeField] private GameObject _emptyFollowPoint;
        
        public Camera Camera => _camera;
        public CinemachineFreeLook Controller => _controller;
        public Vector3 FollowPointOffsetFromPlayer => _followPointOffsetFromPlayer;
        public GameObject EmptyCameraParent => _emptyCameraParent;
        public GameObject EmptyFollowPoint => _emptyFollowPoint;
    }
}