using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours.Damagable
{
    public class Damagable : MonoBehaviour, IDamagable 
    {
        public event Action<float> Damaged;
        
        public void TakeDamage(float damage)
        {
            Damaged?.Invoke(damage);
        }
    }
}