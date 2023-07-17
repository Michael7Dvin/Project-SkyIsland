using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        Scene CurrentScene { get; }
        SceneType CurrentSceneType { get; }
        UniTask Load(SceneType type);
    }
}