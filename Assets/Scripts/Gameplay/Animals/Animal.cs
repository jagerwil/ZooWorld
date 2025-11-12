using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using ZooWorld.Gameplay.Animals.Interactions;

namespace ZooWorld.Gameplay.Animals {
    public class Animal : MonoBehaviour, IPoolable {
        [SerializeField] private BaseAnimalInteraction _animalInteraction;
        
        private List<IAnimalComponent> _components;

        public AnimalType AnimalType => _animalInteraction.AnimalType;

        public static event Action<Animal> onDespawnRequested;

        private void Awake() {
            _components = GetComponentsInChildren<IAnimalComponent>().ToList();
        }

        //We can store these animals in some provider in the future
        public void OnSpawned() {
            _animalInteraction.Initialize(RequestDespawn);
            
            foreach (var component in _components) {
                component.Enable();
            }
        }
        
        public void OnDespawned() {
            foreach (var component in _components) {
                component.Disable();
            }
        }

        private void RequestDespawn() {
            onDespawnRequested?.Invoke(this);
        }
    }
}
