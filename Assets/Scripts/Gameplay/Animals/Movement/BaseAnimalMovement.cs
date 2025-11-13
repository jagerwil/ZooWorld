using UnityEngine;
using ZooWorld.Gameplay.Animals.TargetProvider;

namespace ZooWorld.Gameplay.Animals.Movement {
    public abstract class BaseAnimalMovement : MonoBehaviour, IAnimalComponent {
        [SerializeField] private BaseAnimalTargetProvider _targetProvider;
        [SerializeField] protected Rigidbody Rigidbody;
        
        protected Vector3 TargetPosition { get; private set; }
        
        public abstract void Enable();
        public abstract void Disable();
        
        public abstract void ChangeHorizontalDirection(Vector2 newDirection);

        protected void GetNewTarget(float minDistanceToTarget) {
            TargetPosition = _targetProvider.GetTargetPoint(minDistanceToTarget);
        }
    }
}
