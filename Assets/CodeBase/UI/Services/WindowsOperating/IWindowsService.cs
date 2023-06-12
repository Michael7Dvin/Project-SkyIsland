using Cysharp.Threading.Tasks;
using UI.Windows;

namespace UI.Services.WindowsOperating
{
    public interface IWindowsService
    {
        UniTask OpenWindow(WindowType type);
        void CloseWindow(WindowType type);
    }
}