using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Addresses
{
    [CreateAssetMenu(fileName = "UI Assets Addresses", menuName = "Assets Addresses/UI")]
    public class UIAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public string Canvas { get; private set; }
        [field: SerializeField] public string EventSystem { get; private set; }
        
        [field: SerializeField] public string MainMenuWindow { get; private set; }
        [field: SerializeField] public string SaveSelectionWindow { get; private set; }
        [field: SerializeField] public string PauseWindow { get; private set; }
        [field: SerializeField] public string DeathWindow { get; private set; }
        
        [field: SerializeField] public string HealthBar { get; private set; }
    }
}