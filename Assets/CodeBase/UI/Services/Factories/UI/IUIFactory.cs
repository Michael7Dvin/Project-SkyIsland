using Cysharp.Threading.Tasks;

namespace UI.Services.Factories.UI
{
    public interface IUIFactory
    {
        void Init();
        UniTask WarmUp();
        UniTask RecreateSceneUIObjects();
    }
}