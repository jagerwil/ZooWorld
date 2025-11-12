using UnityEngine;

namespace ZooWorld.Gameplay.Animals.Interactions {
    public class PredatorAnimalInteraction : BaseAnimalInteraction {
        public override AnimalType AnimalType => AnimalType.Predator;

        protected override void InteractWithAnimal(BaseAnimalInteraction interaction) {
            switch (interaction.AnimalType) {
                case AnimalType.Prey:
                    break;
                case AnimalType.Predator:
                    DespawnWithChance();
                    break;
                default:
                    Debug.LogError($"{nameof(PredatorAnimalInteraction)}.{nameof(InteractWithAnimal)}(): "
                                   + $"animal type {AnimalType} is not supported");
                    break;
            }
        }

        private void DespawnWithChance() {
            if (Random.Range(0f, 1f) >= 0.5f) {
                RequestDespawn();
            }
        }
    }
}
