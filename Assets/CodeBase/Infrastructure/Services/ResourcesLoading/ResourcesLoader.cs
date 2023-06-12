using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.ResourcesLoading
{
    public class ResourcesLoader : IResourcesLoader
    {
        private readonly Dictionary<string, Object> _cache = new();
        
        public async UniTask<T> Load<T>(string address) where T : Object
        {
            if (_cache.ContainsKey(address))
                return (T)_cache[address];

            ResourceRequest resourceRequest = Resources.LoadAsync<T>(address);
            await resourceRequest;

            T resource = (T)resourceRequest.asset;
            AddToCache(resource, address);
            
            return resource;
        }

        public void ClearCache() => 
            _cache.Clear();

        private void AddToCache<T>(T resource, string address) where T : Object
        {
            if (_cache.ContainsKey(address))
                _cache.Add(address, resource);
        }
    }
}