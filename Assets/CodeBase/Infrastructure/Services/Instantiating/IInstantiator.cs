using UnityEngine;

namespace Infrastructure.Services.Instantiating
{
    public interface IInstantiator
    {
        T Instantiate<T>(T original) where T : Object;
        T Instantiate<T>(T original, Transform parent) where T : Object;
        T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object;
        T Instantiate<T>(T original, Transform parent, Vector3 position, Quaternion rotation) where T : Object;
    }
}