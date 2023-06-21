using Cysharp.Threading.Tasks;

namespace UI.Services.UIFactory
{
    public interface IUIFactory
    {
        void Init();
        UniTask WarmUp();
        UniTask RecreateSceneUIObjects();
    }
}