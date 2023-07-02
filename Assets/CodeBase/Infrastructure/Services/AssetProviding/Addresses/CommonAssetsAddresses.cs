using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetProviding.Addresses
{
    [CreateAssetMenu(fileName = "Common Assets Addresses", menuName = "Assets Addresses/Common")]
    public class CommonAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject EmptyGameObject { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Hero { get; private set; }
    }
}