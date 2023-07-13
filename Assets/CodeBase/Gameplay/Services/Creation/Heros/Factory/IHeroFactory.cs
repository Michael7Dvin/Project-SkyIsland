using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Services.Creation.Heros.Factory
{
    public interface IHeroFactory
    {
        UniTask WarmUp();
        UniTask<Gameplay.Heros.Hero> Create(Vector3 position, Quaternion rotation);
    }
}