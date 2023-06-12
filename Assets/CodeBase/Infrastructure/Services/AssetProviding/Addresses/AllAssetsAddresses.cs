using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Addresses
{
    [CreateAssetMenu(fileName = "All Assets Addresses", menuName = "Assets Addresses/All")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public CommonAssetsAddresses CommonAssets { get; private set;}
        [field: SerializeField] public UIAssetsAddresses UIAssets { get; private set;}
        [field: SerializeField] public CameraAssetsAddresses CameraAssets { get; private set;}
    }
}