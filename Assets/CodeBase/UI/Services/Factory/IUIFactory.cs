using Cysharp.Threading.Tasks;

namespace UI.Services.Factory
{
    public interface IUIFactory
    {
        void Init();
        UniTask WarmUp();
        UniTask RecreateSceneUIObjects();
    }
}