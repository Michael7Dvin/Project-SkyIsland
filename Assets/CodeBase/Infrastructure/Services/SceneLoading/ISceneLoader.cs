using Cysharp.Threading.Tasks;

namespace Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        void Initailize();
        
        SceneID CurrentSceneID { get; }
        UniTask Load(SceneID id);
    }
}