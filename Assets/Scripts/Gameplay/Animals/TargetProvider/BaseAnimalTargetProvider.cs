using UnityEngine;

namespace ZooWorld.Gameplay.Animals.TargetProvider {
    public abstract class BaseAnimalTargetProvider : MonoBehaviour {
        public abstract Vector3 GetTargetPoint(float minDistanceFromAnimal);
    }
}
