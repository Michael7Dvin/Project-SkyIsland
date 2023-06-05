using UnityEditor;
using UnityEngine;

namespace Infrastructure.Services.SceneLoading
{
    [CreateAssetMenu(fileName = "Scenes Data", menuName = "Scenes Data")]
    public class ScenesData : ScriptableObject
    {
        [SerializeField] private SceneAsset _mainMenu;
        [SerializeField] private SceneAsset _island;

        public string MainMenuSceneName => _mainMenu.name;
        public string IslandSceneName => _island.name;
    }
}