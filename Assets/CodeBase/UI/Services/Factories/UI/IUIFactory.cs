using Cysharp.Threading.Tasks;

namespace UI.Services.Factories.UI
{
    public interface IUIFactory
    {
        UniTask WarmUp();
        UniTask RecreateSceneUIObjects();
    }
}