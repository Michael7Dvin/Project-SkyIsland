using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetProviding.Addresses.UI
{
    [CreateAssetMenu(fileName = "Windows", menuName = "Assets Addresses/UI/Windows")]
    public class WindowsAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject MainMenu { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject SaveSelection { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Pause { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Death { get; private set; }
    }
}