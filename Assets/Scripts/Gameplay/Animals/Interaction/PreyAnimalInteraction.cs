using UnityEngine;
using ZooWorld.Gameplay.Animals.Movement;

namespace ZooWorld.Gameplay.Animals.Interaction {
    public class PreyAnimalInteraction : BaseAnimalInteraction {
        [SerializeField] private BaseAnimalMovement _movement;
        
        public override AnimalType AnimalType => AnimalType.Prey;

        protected override void InteractWithAnimal(BaseAnimalInteraction interaction) {
            switch (interaction.AnimalType) {
                case AnimalType.Prey:
                    BounceOffPrey(interaction);
                    break;
                case AnimalType.Predator:
                    RequestDespawn();
                    break;
                default:
                    Debug.LogError($"{nameof(PreyAnimalInteraction)}.{nameof(InteractWithAnimal)}(): "
                                   + $"animal type {AnimalType} is not supported");
                    break;
            }
        }

        private void BounceOffPrey(BaseAnimalInteraction interaction) {
            var deltaVector = interaction.transform.position - transform.position;
            deltaVector.y = 0f;

            var deltaRotation = Quaternion.FromToRotation(deltaVector, transform.forward);
            var deltaAngle = deltaRotation.eulerAngles.y;

            if (deltaAngle > 180f) {
                deltaAngle -= 360f;
            }

            Quaternion bounceRotation;
            var angleAbs = Mathf.Abs(deltaAngle);
            if (angleAbs < 90f) {
                var angleSign = Mathf.Sign(deltaAngle);
                var rotationAngle = angleSign * 2f * (90f - Mathf.Abs(deltaAngle));
                bounceRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            }
            else {
                bounceRotation = Quaternion.identity;
            }

            var moveDirection = bounceRotation * Vector3.forward;
            _movement.ChangeHorizontalDirection(new Vector2(moveDirection.x, moveDirection.z));
        }
    }
}
