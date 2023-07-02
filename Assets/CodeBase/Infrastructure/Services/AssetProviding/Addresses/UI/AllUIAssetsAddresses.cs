using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetProviding.Addresses.UI
{
    [CreateAssetMenu(fileName = "All", menuName = "Assets Addresses/UI/All")]
    public class AllUIAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject Canvas { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject EventSystem { get; private set; }
        
        [field: SerializeField] public WindowsAssetsAddresses Windows { get; private set; }
        [field: SerializeField] public HUDAssetsAddresses HUD { get; private set; }
        [field: SerializeField] public BackgroundsAssetsAddresses Backgrounds { get; private set; }
    }
}