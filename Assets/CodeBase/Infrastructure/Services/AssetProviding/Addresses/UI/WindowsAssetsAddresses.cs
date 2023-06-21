using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Addresses.UI
{
    [CreateAssetMenu(fileName = "Windows", menuName = "Assets Addresses/UI/Windows")]
    public class WindowsAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public string MainMenuWindow { get; private set; }
        [field: SerializeField] public string SaveSelectionWindow { get; private set; }
        [field: SerializeField] public string PauseWindow { get; private set; }
        [field: SerializeField] public string DeathWindow { get; private set; }
    }
}