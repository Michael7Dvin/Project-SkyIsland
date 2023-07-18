using Cysharp.Threading.Tasks;
using UI.Windows;
using UI.Windows.Base.Window;

namespace UI.Services.Factories.Window
{
    public interface IWindowFactory
    {
        UniTask WarmUp();
        UniTask<IWindow> Create(WindowType type);
    }
}