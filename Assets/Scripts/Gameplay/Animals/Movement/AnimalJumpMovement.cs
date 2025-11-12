using Cysharp.Threading.Tasks;
using MafiaGame.Extensions;
using UnityEngine;
using ZooWorld.Gameplay.Animals.TargetProvider;

namespace ZooWorld.Gameplay.Animals.Movement {
    [RequireComponent(typeof(AnimalTargetProvider))]
    public class AnimalJumpMovement : BaseAnimalMovement {
        #region Fields
        [SerializeField] private float _jumpDistance;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _intervalBetweenJumps;

        private JumpMovementState _state = JumpMovementState.PreparedToJump;
        private Vector3 _jumpVelocity;
        #endregion
        
        
        #region Unity callbacks
        private void Awake() {
            var gravity = -1f * Physics.gravity.y;
            var jumpDuration = Mathf.Sqrt((2f * _jumpDistance) / gravity);
            
            _jumpVelocity = new Vector3(0f,
                                        _jumpDistance / jumpDuration, 
                                        gravity * jumpDuration);
        }

        private void FixedUpdate() {
            if (_state != JumpMovementState.Jumping) {
                return;
            }
            
            if (Rigidbody.linearVelocity.y.ApproximatelyZero()) {
                SetState(JumpMovementState.IdleAfterJump);
            }
        }
        #endregion

        
        #region Public methods
        public override void Enable() {
            SetState(JumpMovementState.IdleAfterJump);
        }

        public override void Disable() {
            SetState(JumpMovementState.Disabled);
        }
        #endregion

        #region Private methods
        private void SetState(JumpMovementState state) {
            _state = state;
            switch (state) {
                case JumpMovementState.PreparedToJump:
                    GetNewTarget(_jumpDistance);
                    SetState(JumpMovementState.Jumping);
                    break;
                case JumpMovementState.Jumping:
                    SetupVelocity();
                    break;
                case JumpMovementState.IdleAfterJump:
                    Rigidbody.linearVelocity = Vector3.zero;
                    WaitJumpingIntervalAsync().Forget();
                    break;
            }
        }

        private void SetupVelocity() {
            var targetDeltaVector = TargetPosition - Rigidbody.position;
            var rotation = Quaternion.LookRotation(targetDeltaVector, Vector3.up).eulerAngles;
            rotation.x = 0f;
            rotation.z = 0f;

            Rigidbody.rotation = Quaternion.Euler(rotation);
            Rigidbody.linearVelocity = Rigidbody.rotation * _jumpVelocity;
        }

        private async UniTask WaitJumpingIntervalAsync() {
            await UniTask.WaitForSeconds(_intervalBetweenJumps);
            SetState(JumpMovementState.PreparedToJump);
        }
        #endregion
        
        private enum JumpMovementState {
            PreparedToJump = 0,
            Jumping = 1,
            IdleAfterJump = 2,
            Disabled = 3,
        }
    }
}
