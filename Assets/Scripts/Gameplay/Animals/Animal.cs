using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ZooWorld.Gameplay.Animals {
    public class Animal : MonoBehaviour, IPoolable {
        private List<IAnimalComponent> _components;

        private void Awake() {
            _components = GetComponentsInChildren<IAnimalComponent>().ToList();
        }

        //We can store these animals in some provider in the future
        public void OnSpawned() {
            foreach (var component in _components) {
                component.Enable();
            }
        }
        
        public void OnDespawned() {
            foreach (var component in _components) {
                component.Disable();
            }
        }
    }
}
