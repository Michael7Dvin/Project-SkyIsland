using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    public class DamageNotifier : MonoBehaviour, IDamageNotifier 
    {
        public event Action<float> Damaged;
        
        public void Damage(float damage)
        {
            Damaged?.Invoke(damage);
        }
    }
}