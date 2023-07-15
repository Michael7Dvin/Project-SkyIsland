using Gameplay.Heros;

namespace Gameplay.Services.HeroProviding
{
    public class HeroProvider : IHeroProvider 
    {
        public Hero Hero { get; private set; }
        
        public void Set(Hero hero)
        {
            Hero = hero;
            hero.Destroyable.Destroyed += Remove;
        }

        private void Remove()
        {
            if (Hero != null) 
                Hero.Destroyable.Destroyed -= Remove;

            Hero = null;
        }
    }
}