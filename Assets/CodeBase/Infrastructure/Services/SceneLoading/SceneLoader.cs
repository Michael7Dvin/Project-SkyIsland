using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Logging;
using Infrastructure.Services.StaticDataProviding;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Infrastructure.Services.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        private Dictionary<SceneID, AssetReference> _scenes;
        
        private readonly AllScenesData _allScenesData;
        private readonly ICustomLogger _logger;

        public SceneLoader(IStaticDataProvider staticDataProvider, ICustomLogger logger)
        {
            _allScenesData = staticDataProvider.ScenesData;
            _logger = logger;
        }

        public SceneID CurrentSceneID { get; private set; }

        public void Initailize()
        {
            SceneData mainMenu = _allScenesData.MainMenu;
            SceneData island = _allScenesData.Island;

            _scenes = new Dictionary<SceneID, AssetReference>
            {
                { mainMenu.SceneID, mainMenu.AssetReference },
                { island.SceneID, island.AssetReference },
            };
        }

        public async UniTask Load(SceneID id)
        {
            if (_scenes.ContainsKey(id) == false)
            {
                _logger.LogError($"Unable to load scene. Can't find {nameof(SceneID)}: '{id}' in {nameof(_scenes)}");
                return;
            }

            AssetReference sceneReference = _scenes[id];

            await Load(sceneReference);
            CurrentSceneID = id;
        }

        private async UniTask Load(AssetReference sceneReference)
        {
            if (sceneReference.RuntimeKeyIsValid() == false)
            {
                _logger.LogError($"Unable to load scene. {nameof(AssetReference)} is null");
                return;
            }
            
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneReference);
            await handle.Task;
        }
    }
}