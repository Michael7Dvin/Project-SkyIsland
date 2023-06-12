using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Addresses
{
    [CreateAssetMenu(fileName = "Camera Assets Addresses", menuName = "Assets Addresses/Camera")]
    public class CameraAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public string Camera { get; private set; }
        [field: SerializeField] public string FreeLookController { get; private set; }
    }
}