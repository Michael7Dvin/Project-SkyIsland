using Cysharp.Threading.Tasks;
using Zenject;

namespace Infrastructure.Services.LevelLoading.WarmUpping
{
    public interface IWarmUpper : IInitializable
    {
        LevelType LevelType { get; }
        UniTask WarmUp();
    }
}