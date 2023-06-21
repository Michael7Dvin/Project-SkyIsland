using Cysharp.Threading.Tasks;
using UI.Services.Mediating;
using UI.Windows.Base.Window;
using UnityEngine;

namespace UI.Windows.Factory
{
    public interface IWindowFactory
    {
        void Init(IMediator mediator);
        UniTask WarmUp();
        void ResetCanvas(Canvas canvas);

        UniTask<IWindow> Create(WindowType type);
    }
}