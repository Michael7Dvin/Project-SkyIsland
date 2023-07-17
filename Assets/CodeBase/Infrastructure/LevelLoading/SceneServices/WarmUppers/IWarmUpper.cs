using Cysharp.Threading.Tasks;
using Zenject;

namespace Infrastructure.LevelLoading.SceneServices.WarmUppers
{
    public interface IWarmUpper : IInitializable
    {
        UniTask WarmUp();
    }
}