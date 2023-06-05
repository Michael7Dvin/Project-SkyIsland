using UnityEngine;

namespace Infrastructure.Services.Instantiating
{
    public class Instantiator : IInstantiator
    {
        public T Instantiate<T>(T original) where T : Object => 
            Object.Instantiate(original);
        public T Instantiate<T>(T original, Transform parent) where T : Object => 
            Object.Instantiate(original, parent);
  
        public T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object => 
            Object.Instantiate(original, position, rotation);
   
        public T Instantiate<T>(T original, Transform parent, Vector3 position, Quaternion rotation) where T : Object => 
            Object.Instantiate(original, position, rotation, parent);
    }
}