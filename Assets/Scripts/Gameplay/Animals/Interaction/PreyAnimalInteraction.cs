using UnityEngine;

namespace ZooWorld.Gameplay.Animals.Interaction {
    public class PreyAnimalInteraction : BaseAnimalInteraction {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _bounceMultiplier = 2f;
        
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
                var rotationAngle = angleSign * -2f * (90f - Mathf.Abs(deltaAngle));
                bounceRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            }
            else {
                bounceRotation = Quaternion.identity;
            }

            _rigidbody.rotation *= bounceRotation;

            var velocity = bounceRotation * _rigidbody.linearVelocity.normalized;
            velocity.x *= _bounceMultiplier;
            velocity.z *= _bounceMultiplier;
            _rigidbody.linearVelocity = velocity;
        }
    }
}
