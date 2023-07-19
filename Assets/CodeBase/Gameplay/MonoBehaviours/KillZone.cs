using UnityEngine;

namespace Gameplay.MonoBehaviours
{
    [RequireComponent(typeof(Collider))]
    public class KillZone : MonoBehaviour
    {
        private Collider _damageArea;
        private void Awake()
        {
            _damageArea = GetComponent<Collider>();
            _damageArea.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Damagable damagableNotifier))
                damagableNotifier.TakeDamage(float.MaxValue);
        }
    }
}