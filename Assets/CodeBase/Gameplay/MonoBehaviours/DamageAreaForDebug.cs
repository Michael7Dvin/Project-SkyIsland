﻿using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    [RequireComponent(typeof(Collider))]
    public class DamageAreaForDebug : MonoBehaviour
    {
        [Range(1, 100)]
        [SerializeField] private float Damage;
        
        private Collider _damageArea;
        private readonly List<Damagable> _damageNotifiers = new();
        
        private void Awake()
        {
            _damageArea = GetComponent<Collider>();
            _damageArea.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Damagable damageNotifier))
                _damageNotifiers.Add(damageNotifier);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Damagable damageNotifier))
                _damageNotifiers.Remove(damageNotifier);
        }

        private void Update()
        {
            foreach (Damagable damageNotifier in _damageNotifiers)
            {
                damageNotifier.TakeDamage(Damage * Time.deltaTime);
            }
        }
    }
}