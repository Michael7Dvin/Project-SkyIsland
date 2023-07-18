using UnityEngine;

namespace Infrastructure.Services.SceneLoading
{
    [CreateAssetMenu(fileName = "All Scenes Data", menuName = "Scenes Data/All")]
    public class AllScenesData : ScriptableObject
    {
        [field: SerializeField] public SceneData MainMenu { get; private set; }
        [field: SerializeField] public SceneData Island { get; private set; }
    }
}