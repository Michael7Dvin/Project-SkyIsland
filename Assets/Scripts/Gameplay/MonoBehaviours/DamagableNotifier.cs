using System;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    public class DamagableNotifier : MonoBehaviour, IDamagableNotifier 
    {
        public event Action<float> Damaged;
        
        public void Damage(float damage)
        {
            Damaged?.Invoke(damage);
        }
    }
}