using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ZooWorld.Gameplay.Animals.Movement {
    public class AnimalLinearMovement : BaseAnimalMovement {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _newTargetInterval = 1f;

        private CancellationTokenSource _cancelToken;
        private Vector3 _movementVector;

        private void OnDestroy() {
            _cancelToken?.Dispose();
        }

        private void FixedUpdate() {
            Rigidbody.linearVelocity = _movementVector;
        }

        public override void Enable() {
            FindNewTargetEndlessAsync().Forget();
        }
        
        public override void Disable() {
            _cancelToken?.Cancel();
            Rigidbody.linearVelocity = Vector3.zero;
        }

        private async UniTask FindNewTargetEndlessAsync() {
            _cancelToken = new CancellationTokenSource();

            while (true) {
                GetNewTarget(_moveSpeed * _newTargetInterval);

                var movementDirection = (TargetPosition - transform.position).normalized;
                movementDirection.y = 0;
                
                Rigidbody.rotation = Quaternion.LookRotation(movementDirection);
                _movementVector = Rigidbody.rotation * new Vector3(0f, 0f, _moveSpeed);
                
                await UniTask.WaitForSeconds(_newTargetInterval, cancellationToken: _cancelToken.Token);
            }
        }
    }
}
