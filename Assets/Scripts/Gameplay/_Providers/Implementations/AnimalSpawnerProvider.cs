using UnityEngine;
using ZooWorld.Gameplay.Animals.Spawners;

namespace ZooWorld.Gameplay._Providers.Implementations {
    public class AnimalSpawnerProvider : IAnimalSpawnerProvider {
        public AnimalSpawner AnimalSpawner { get; }
        
        public AnimalSpawnerProvider(AnimalSpawner animalSpawner) {
            AnimalSpawner = animalSpawner;
        }
    }
}
