using Cysharp.Threading.Tasks;
using Gameplay.Heros;
using UnityEngine;

namespace Gameplay.Services.Spawners.Heros
{
    public interface IHeroSpawner
    {
        UniTask<Hero> Spawn(Vector3 position, Quaternion rotation);
    }
}