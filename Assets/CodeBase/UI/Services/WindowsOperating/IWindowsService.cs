using Cysharp.Threading.Tasks;
using UI.Windows;
using UI.Windows.Base;
using UI.Windows.Base.Window;

namespace UI.Services.WindowsOperating
{
    public interface IWindowsService
    {
        UniTask<IWindow> OpenWindow(WindowType type);
        void CloseWindow(WindowType type);
    }
}