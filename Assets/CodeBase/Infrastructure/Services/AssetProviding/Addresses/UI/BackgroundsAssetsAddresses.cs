using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Addresses.UI
{
    [CreateAssetMenu(fileName = "Backgrounds", menuName = "Assets Addresses/UI/Backgrounds")]
    public class BackgroundsAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public string MainMenuBackground { get; private set; }
        [field: SerializeField] public string PauseBackground { get; private set; }
        [field: SerializeField] public string DeathBackground { get; private set; }
    }
}