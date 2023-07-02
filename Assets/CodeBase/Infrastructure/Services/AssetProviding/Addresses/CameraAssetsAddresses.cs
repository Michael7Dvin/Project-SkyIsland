using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetProviding.Addresses
{
    [CreateAssetMenu(fileName = "Camera Assets Addresses", menuName = "Assets Addresses/Camera")]
    public class CameraAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Camera { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject FreeLookController { get; private set; }
    }
}