using UnityEngine;

namespace Infrastructure.Services.Instantiating
{
    public interface IInstantiator
    {
        T Instantiate<T>();
        
        GameObject InstantiatePrefab(GameObject original);
        GameObject InstantiatePrefab(GameObject original, Transform parent);
        GameObject InstantiatePrefab(GameObject original, Vector3 position, Quaternion rotation);
        GameObject InstantiatePrefab(GameObject original, Vector3 position, Quaternion rotation, Transform parent);
        
        T InstantiatePrefabForComponent<T>(T original) where T : Component;
        T InstantiatePrefabForComponent<T>(T original, Transform parent) where T : Component;
        T InstantiatePrefabForComponent<T>(T original, Vector3 position, Quaternion rotation) where T : Component;
        T InstantiatePrefabForComponent<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Component;
    }
}