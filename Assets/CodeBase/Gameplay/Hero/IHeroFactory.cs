using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Hero
{
    public interface IHeroFactory
    {
        UniTask WarmUp();
        UniTask<Hero> Create(Vector3 position, Quaternion rotation);
    }
}