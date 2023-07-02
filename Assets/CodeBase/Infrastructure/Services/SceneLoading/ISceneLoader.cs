using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        UniTask Load(SceneType type);
    }
}