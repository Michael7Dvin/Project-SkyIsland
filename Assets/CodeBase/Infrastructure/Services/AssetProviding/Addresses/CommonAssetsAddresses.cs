using UnityEngine;

namespace Infrastructure.Services.AssetProviding.Addresses
{
    [CreateAssetMenu(fileName = "Common Assets Addresses", menuName = "Assets Addresses/Common")]
    public class CommonAssetsAddresses : ScriptableObject
    {
        [field: SerializeField] public string EmptyGameObject { get; private set; }
        [field: SerializeField] public string Hero { get; private set; }
    }
}