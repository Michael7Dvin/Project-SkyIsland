using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.Services.SceneLoading
{
    [CreateAssetMenu(fileName = "Scene Data", menuName = "Scenes Data/Scene Data")]
    public class SceneData : ScriptableObject
    {
        [field: SerializeField] public AssetReference AssetReference { get; private set; }
        [field: SerializeField] public SceneType SceneType { get; private set; }
    }
}