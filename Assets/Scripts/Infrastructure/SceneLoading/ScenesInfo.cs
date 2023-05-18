using UnityEditor;
using UnityEngine;

namespace Infrastructure.SceneLoading
{
    [CreateAssetMenu(fileName = "Scenes Info", menuName = "Scenes Info")]
    public class ScenesInfo : ScriptableObject
    {
        [SerializeField] private SceneAsset _mainMenu;
        [SerializeField] private SceneAsset _island;

        public string MainMenuSceneName => _mainMenu.name;
        public string IslandSceneName => _island.name;
    }
}