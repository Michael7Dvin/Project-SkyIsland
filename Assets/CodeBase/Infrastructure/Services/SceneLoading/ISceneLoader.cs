using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        void Initailize();
        
        SceneType CurrentScene { get; }
        UniTask Load(SceneType type);
    }
}