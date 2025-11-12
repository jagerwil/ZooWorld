using System;
using Jagerwil.Core.Architecture.Factories;
using JetBrains.Annotations;
using UnityEngine;
using ZooWorld.Gameplay.Animals;

namespace ZooWorld.Gameplay._Factories {
    public interface IAnimalFactory : IGamePrefabFactory<Animal> {
        public event Action<Animal> OnDespawned;
        
        [CanBeNull]
        public Animal Spawn(AnimalId id, Vector3 position);
        
        [CanBeNull]
        public Animal SpawnRandom(Vector3 position);
    }
}
