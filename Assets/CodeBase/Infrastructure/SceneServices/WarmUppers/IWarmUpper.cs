using Cysharp.Threading.Tasks;

namespace Infrastructure.SceneServices.WarmUppers
{
    public interface IWarmUpper
    {
        UniTask WarmUp();
    }
}