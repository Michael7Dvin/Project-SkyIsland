using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.AssetProviding.Addresses.UI
{
    [CreateAssetMenu(fileName = "HUD", menuName = "Assets Addresses/UI/HUD")]
    public class HUDAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public AssetReferenceGameObject HealthBar { get; private set; }
    }
}