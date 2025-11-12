using System;
using UnityEngine;

namespace ZooWorld.Gameplay.Animals.Interactions {
    public abstract class BaseAnimalInteraction : MonoBehaviour, IAnimalComponent {
        public abstract AnimalType AnimalType { get; }

        private Action _despawnAction;
        private bool _canInteract;

        private void OnCollisionEnter(Collision other) {
            if (!_canInteract) {
                return;
            }

            var animalInteraction = other.gameObject.GetComponent<BaseAnimalInteraction>();
            if (animalInteraction && animalInteraction._canInteract) {
                InteractWithAnimal(animalInteraction);
            }
        }

        public virtual void Initialize(Action despawnAction) {
            _despawnAction = despawnAction;
        }

        public virtual void Enable() {
            _canInteract = true;
        }

        public virtual void Disable() {
            _canInteract = false;
        }

        protected void RequestDespawn() {
            _despawnAction?.Invoke();
        }

        protected abstract void InteractWithAnimal(BaseAnimalInteraction interaction);
    }
}
