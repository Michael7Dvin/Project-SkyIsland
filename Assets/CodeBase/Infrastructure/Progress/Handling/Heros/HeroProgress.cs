using System;

namespace Infrastructure.Progress.Handling.Heros
{
    [Serializable]
    public class HeroProgress
    {
        public bool IsEmpty = true;
        public float CurrentHealth;
    }
}