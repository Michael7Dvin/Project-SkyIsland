using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Observable;
using Gameplay.MonoBehaviours;
using UnityEngine;

namespace Gameplay.GroundTypeObserving
{
    public class GroundTypeObserver : IGroundTypeObserver, IDisposable
    {
        private const int SetToNotGroundedDelayInMilliseconds = 50;
        private readonly Observable<GroundType> _currentCurrentGroundType = new();
        
        private readonly LayerMask _groundLayerMask;
        private readonly ICollisionObserver _collisionObserver;
        
        private bool _isColliding;
        private Task _delayTask;
        private CancellationTokenSource _cancellationTokenSource;

        public GroundTypeObserver(ICollisionObserver collisionObserver, LayerMask groundLayerMask)
        {
            _collisionObserver = collisionObserver;
            _groundLayerMask = groundLayerMask;

            _collisionObserver.CollisionStayed += OnCollisionStay;
            _collisionObserver.CollisionExited += OnCollisionExit;
            _collisionObserver.Destroyed += Dispose;
        }
        
        public IReadOnlyObservable<GroundType> CurrentGroundType => _currentCurrentGroundType;

        public void Dispose()
        {
            _collisionObserver.CollisionStayed -= OnCollisionStay;
            _collisionObserver.CollisionExited -= OnCollisionExit;
            _collisionObserver.Destroyed -= Dispose;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & _groundLayerMask) != 0)
            {
                if (_isColliding == false)
                {
                    _isColliding = true;
                    _cancellationTokenSource?.Cancel();
                    _currentCurrentGroundType.Value = GroundType.Ground;   
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (((1 << collision.gameObject.layer) & _groundLayerMask) != 0)
            {
                _isColliding = false;

                _cancellationTokenSource?.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                _delayTask = DelayBeforeNotGrounded(_cancellationTokenSource.Token);
            }
        }

        private async Task DelayBeforeNotGrounded(CancellationToken cancellationToken)
        {
            await Task.Delay(SetToNotGroundedDelayInMilliseconds, cancellationToken);
    
            if (_isColliding == false) 
                _currentCurrentGroundType.Value = GroundType.Air;
        }
    }
}