using Cysharp.Threading.Tasks;
using Gameplay.Healths;
using UnityEngine;

namespace UI.HUD.Factory
{
    public interface IHUDFactory
    {
        UniTask WarmUp();
        void ResetCanvas(Canvas canvas);
        UniTask<HealthBar> CreateHealthBar(IHealth health);
    }
}