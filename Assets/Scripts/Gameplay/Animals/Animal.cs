using UnityEngine;
using Zenject;

namespace ZooWorld.Gameplay.Animals {
    public class Animal : MonoBehaviour, IPoolable {
        public void Initialize() {
            
        }
        
        //We can store these animals in some provider in the future
        public void OnSpawned() {
            
        }
        
        public void OnDespawned() {
            
        }
    }
}
