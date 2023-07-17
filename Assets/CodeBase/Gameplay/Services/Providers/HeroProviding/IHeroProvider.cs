using Common.Observable;
using Gameplay.Heros;

namespace Gameplay.Services.Providers.HeroProviding
{
    public interface IHeroProvider
    {
        public IReadOnlyObservable<Hero> Hero { get; }

        public void Set(Hero hero);
    }
}