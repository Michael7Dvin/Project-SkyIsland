using Cysharp.Threading.Tasks;
using Gameplay.Healths;
using UI.HUD;
using UnityEngine;

namespace UI.Services.Factories.HUD
{
    public interface IHUDFactory
    {
        UniTask WarmUp();
        void ResetCanvas(Canvas canvas);
        UniTask<HealthBar> CreateHealthBar(IHealth health);
    }
}