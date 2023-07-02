using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetProviding.Addresses.UI
{
    [CreateAssetMenu(fileName = "Windows", menuName = "Assets Addresses/UI/Windows")]
    public class WindowsAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject MainMenuWindowView { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject SaveSelectionWindowView { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject PauseWindowView { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject DeathWindowView { get; private set; }
    }
}