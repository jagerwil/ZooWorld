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
            Rigidbody.linearVelocity = Rigidbody.rotation * _movementVector;
        }

        public override void Enable() {
            FindNewTargetEndlessAsync().Forget();
        }
        
        public override void Disable() {
            _cancelToken?.Cancel();
            Rigidbody.linearVelocity = Vector3.zero;
        }

        public override void ChangeHorizontalDirection(Vector2 newDirection) {
            SetRotation(newDirection);
        }

        private async UniTask FindNewTargetEndlessAsync() {
            _cancelToken = new CancellationTokenSource();

            while (true) {
                GetNewTarget(_moveSpeed * _newTargetInterval);

                var movementDirection = (TargetPosition - transform.position).normalized;
                SetRotation(movementDirection);
                _movementVector = new Vector3(0f, 0f, _moveSpeed);
                
                await UniTask.WaitForSeconds(_newTargetInterval, cancellationToken: _cancelToken.Token);
            }
        }

        private void SetRotation(Vector3 movementDirection) {
            movementDirection.y = 0;
            Rigidbody.rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        }
    }
}
