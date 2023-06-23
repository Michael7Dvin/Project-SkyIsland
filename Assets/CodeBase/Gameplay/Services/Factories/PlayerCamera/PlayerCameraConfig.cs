using UnityEngine;

namespace Gameplay.Services.Factories.PlayerCamera
{
    [CreateAssetMenu(fileName = "Player Camera Config", menuName = "Configs/Camera")]
    public class PlayerCameraConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 FollowPointOffsetFromPlayer { get; private set; }
    }
}