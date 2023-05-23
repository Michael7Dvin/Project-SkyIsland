using Common.Observable;
using UnityEngine;

namespace Gameplay.BodyEnvironmentObserving.GroundDetection
{
    [RequireComponent(typeof(Rigidbody))]
    public class GroundDetector : MonoBehaviour, IGroundDetector
    {
        [SerializeField] private LayerMask _groundLayerMask;
        private Rigidbody _rigidbody;
        
        private readonly Observable<bool> _grounded = new();

        public IReadOnlyObservable<bool> Grounded => _grounded;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & _groundLayerMask) != 0)
            {
                _grounded.Value = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & _groundLayerMask) != 0)
            {
                _grounded.Value = false;
            }
        }
    }
}