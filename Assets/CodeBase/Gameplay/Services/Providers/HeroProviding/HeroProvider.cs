using Common.Observable;
using Gameplay.Heros;

namespace Gameplay.Services.Providers.HeroProviding
{
    public class HeroProvider : IHeroProvider
    {
        private readonly Observable<Hero> _hero = new();

        public IReadOnlyObservable<Hero> Hero => _hero;

        public void Set(Hero hero)
        {
            _hero.Value = hero;
            hero.Destroyable.Destroyed += Remove;
        }

        private void Remove()
        {
            if (_hero.Value != null) 
                _hero.Value.Destroyable.Destroyed -= Remove;

            _hero.Value = null;
        }
    }
}