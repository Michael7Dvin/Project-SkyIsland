using Gameplay.Heros;

namespace Gameplay.Services.HeroProviding
{
    public interface IHeroProvider
    {
        public Hero Hero { get; }

        public void Set(Hero hero);
    }
}