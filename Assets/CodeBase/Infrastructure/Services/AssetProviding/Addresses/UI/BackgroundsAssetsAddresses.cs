using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetProviding.Addresses.UI
{
    [CreateAssetMenu(fileName = "Backgrounds", menuName = "Assets Addresses/UI/Backgrounds")]
    public class BackgroundsAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject MainMenuBackground { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject PauseBackground { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject DeathBackground { get; private set; }
    }
}