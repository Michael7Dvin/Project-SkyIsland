using Infrastructure.Services.SceneLoading;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Instantiating
{
    public class Instantiator : IInstantiator
    {
        private readonly SceneContextRegistry _sceneContextRegistry;
        private readonly ISceneLoader _sceneLoader;

        public Instantiator(SceneContextRegistry sceneContextRegistry, ISceneLoader sceneLoader)
        {
            _sceneContextRegistry = sceneContextRegistry;
            _sceneLoader = sceneLoader;
        }

        private DiContainer CurrentDiContainer => 
            _sceneContextRegistry.GetContainerForScene(_sceneLoader.CurrentScene);

        public T Instantiate<T>() =>
            CurrentDiContainer.Instantiate<T>();

        public GameObject InstantiatePrefab(GameObject original) => 
            CurrentDiContainer.InstantiatePrefab(original);

        public GameObject InstantiatePrefab(GameObject original, Transform parent) => 
            CurrentDiContainer.InstantiatePrefab(original, parent);

        public GameObject InstantiatePrefab(GameObject original, Vector3 position, Quaternion rotation) => 
            CurrentDiContainer.InstantiatePrefab(original, position, rotation, null);

        public GameObject InstantiatePrefab(GameObject original, Vector3 position, Quaternion rotation, Transform parent) => 
            CurrentDiContainer.InstantiatePrefab(original, position, rotation, parent);

        public T InstantiatePrefabForComponent<T>(T original) where T : Component =>
            CurrentDiContainer.InstantiatePrefabForComponent<T>(original);
        
        public T InstantiatePrefabForComponent<T>(T original, Transform parent) where T : Component => 
            CurrentDiContainer.InstantiatePrefabForComponent<T>(original, parent);
  
        public T InstantiatePrefabForComponent<T>(T original, Vector3 position, Quaternion rotation) where T : Component => 
            CurrentDiContainer.InstantiatePrefabForComponent<T>(original, position, rotation, null);

        public T InstantiatePrefabForComponent<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Component => 
            CurrentDiContainer.InstantiatePrefabForComponent<T>(original, position, rotation, parent);
    }
}