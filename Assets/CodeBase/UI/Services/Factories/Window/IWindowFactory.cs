using Cysharp.Threading.Tasks;
using UI.Services.Operating;
using UI.Windows;
using UI.Windows.Base.Window;
using UnityEngine;

namespace UI.Services.Factories.Window
{
    public interface IWindowFactory
    {
        UniTask WarmUp();
        UniTask<IWindow> Create(WindowType type);
    }
}