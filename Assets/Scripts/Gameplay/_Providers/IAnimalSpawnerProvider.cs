using UnityEngine;
using ZooWorld.Gameplay.Animals.Spawners;

namespace ZooWorld.Gameplay._Providers {
    public interface IAnimalSpawnerProvider {
        public AnimalSpawner AnimalSpawner { get; }
    }
}
