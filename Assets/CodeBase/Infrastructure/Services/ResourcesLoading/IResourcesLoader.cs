using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.ResourcesLoading
{
    public interface IResourcesLoader
    {
        UniTask<T> Load<T>(string address) where T : Object;

        void ClearCache();
    }
}