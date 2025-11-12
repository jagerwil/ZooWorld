using UnityEngine;

namespace ZooWorld.Gameplay.Animals.TargetProvider {
    public abstract class BaseAnimalTargetProvider : MonoBehaviour, IAnimalComponent {
        public abstract Vector3 GetTargetPoint(float minDistanceFromAnimal);
        
        public abstract void Enable();
        public abstract void Disable();
    }
}
