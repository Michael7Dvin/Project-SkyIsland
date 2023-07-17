using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using UnityEngine;

namespace Gameplay.Services.Factories.Heros
{
    public interface IHeroFactory
    {
        UniTask WarmUp();
        UniTask<Hero> Create(Vector3 position, Quaternion rotation);
    }
}