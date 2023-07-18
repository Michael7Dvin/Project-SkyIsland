﻿using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Logging;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure.Services.ResourcesLoading
{
    public class AddressablesLoader : IAddressablesLoader
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cachedAssets = new();
        private readonly ICustomLogger _logger;

        public AddressablesLoader(ICustomLogger logger)
        {
            _logger = logger;
        }

        public async UniTask<GameObject> LoadGameObject(AssetReferenceGameObject assetReference)
        {
            if (assetReference.RuntimeKeyIsValid() == false)
            {
                _logger.LogError("Unable to load GameObject. AssetReference is null");
                return null;
            }

            string assetID = assetReference.AssetGUID;
            
            if (_cachedAssets.ContainsKey(assetID))
                return (GameObject) _cachedAssets[assetID].Result;

            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
            await handle.Task;
            _cachedAssets.Add(assetID, handle);
            return handle.Result;
        }

        public async UniTask<T> LoadComponent<T>(AssetReferenceGameObject assetReference) where T : Component
        {
            GameObject gameObject = await LoadGameObject(assetReference);

            if (gameObject.TryGetComponent(out T component))
                return component;

            _logger.LogError($"Unable to load Component. AssetReference: '{assetReference}' has no required component: '{nameof(T)}' attached");
            return null;
        }

        public void ClearCache()
        {
            foreach (AsyncOperationHandle handle in _cachedAssets.Select(idHandlePair => idHandlePair.Value))
                Addressables.Release(handle);

            _cachedAssets.Clear();
        }
    }
}