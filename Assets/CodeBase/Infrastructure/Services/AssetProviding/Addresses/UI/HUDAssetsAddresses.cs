using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Addresses.UI
{
    [CreateAssetMenu(fileName = "HUD", menuName = "Assets Addresses/UI/HUD")]
    public class HUDAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public string HealthBar { get; private set; }
    }
}