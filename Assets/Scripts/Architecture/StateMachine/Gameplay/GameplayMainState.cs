using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;
using ZooWorld.Gameplay._Providers;

namespace ZooWorld.Architecture.StateMachine.Gameplay {
    public class GameplayMainState : IGameState {
        private readonly IAnimalSpawnerProvider _animalSpawnerProvider;

        public GameplayMainState(IAnimalSpawnerProvider animalSpawnerProvider) {
            _animalSpawnerProvider = animalSpawnerProvider;
        }

        public void Enter() {
            _animalSpawnerProvider.AnimalSpawner.StartSpawning();
        }
        
        public void Exit() { }
    }
}
