using UnityEngine;
using Zenject;
using ZooWorld.Gameplay._Providers;

namespace ZooWorld.Gameplay.Animals.TargetProvider {
    public class AnimalTargetProvider : BaseAnimalTargetProvider {
        [Inject] private ILevelBoundsProvider _levelBoundsProvider;
        
        public override Vector3 GetTargetPoint(float minDistanceFromAnimal) {
            var deltaVector = new Vector3(0f, 0f, minDistanceFromAnimal);
            var randomAngle = new Vector3(0f, Random.Range(0f, 360f), 0f);

            var targetAngle = randomAngle;
            while (targetAngle.y < 720f) {
                var targetPoint = transform.position + Quaternion.Euler(targetAngle) * deltaVector;
                if (_levelBoundsProvider.LevelBounds.Contains(targetPoint)) {
                    return targetPoint;
                }
                
                targetAngle.y += 90f;
            }

            Debug.LogWarning($"{nameof(AnimalTargetProvider)}.{nameof(GetTargetPoint)}(): all random angles are out of bounds!");
            return Vector3.zero;
        }

        public override void Enable() { }
        public override void Disable() { }
    }
}
