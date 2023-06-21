using Infrastructure.Services.AssetProviding.Addresses.UI;
using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Addresses
{
    [CreateAssetMenu(fileName = "All Assets Addresses", menuName = "Assets Addresses/All")]
    public class AllAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public CommonAssetsAddresses Common { get; private set;}
        [field: SerializeField] public AllUIAssetsAddresses UI { get; private set;}
        [field: SerializeField] public CameraAssetsAddresses Camera { get; private set;}
    }
}