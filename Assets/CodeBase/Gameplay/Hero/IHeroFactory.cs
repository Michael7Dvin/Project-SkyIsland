using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Hero
{
    public interface IHeroFactory
    {
        UniTaskVoid WarmUp();
        UniTask<Hero> Create(Vector3 position, Quaternion rotation);
    }
}