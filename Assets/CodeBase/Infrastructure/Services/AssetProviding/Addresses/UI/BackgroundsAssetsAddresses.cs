using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetProviding.Addresses.UI
{
    [CreateAssetMenu(fileName = "Backgrounds", menuName = "Assets Addresses/UI/Backgrounds")]
    public class BackgroundsAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject MainMenu { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Pause { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Death { get; private set; }
    }
}