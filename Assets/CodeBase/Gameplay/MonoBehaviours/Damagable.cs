using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    public class Damagable : MonoBehaviour 
    {
        public event Action<float> Damaged;
        
        public void TakeDamage(float damage)
        {
            Damaged?.Invoke(damage);
        }
    }
}