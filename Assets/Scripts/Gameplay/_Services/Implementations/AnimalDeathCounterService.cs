using System;
using R3;
using UnityEngine;
using ZooWorld.Gameplay._Factories;
using ZooWorld.Gameplay.Animals;

namespace ZooWorld.Gameplay._Services.Implementations {
    public class AnimalDeathCounterService : IAnimalDeathCounterService, IDisposable {
        private readonly ReactiveProperty<int> _deadPredatorsAmount = new();
        private readonly ReactiveProperty<int> _deadPreyAmount = new();
        
        private readonly IAnimalFactory _animalFactory;
        
        public ReadOnlyReactiveProperty<int> DeadPredatorsAmount => _deadPredatorsAmount;
        public ReadOnlyReactiveProperty<int> DeadPreyAmount => _deadPreyAmount;

        public AnimalDeathCounterService(IAnimalFactory animalFactory) {
            _animalFactory = animalFactory;
            animalFactory.OnDespawned += AnimalDespawned;
        }

        public void Dispose() {
            if (_animalFactory != null) {
                _animalFactory.OnDespawned -= AnimalDespawned;
            }
            
            _deadPreyAmount?.Dispose();
            _deadPredatorsAmount?.Dispose();
        }

        public void StartCounting() {
            _deadPreyAmount.Value = 0;
            _deadPredatorsAmount.Value = 0;
        }

        private void AnimalDespawned(Animal animal) {
            switch (animal.AnimalType) {
                case AnimalType.Predator:
                    _deadPredatorsAmount.Value += 1;
                    break;
                case AnimalType.Prey:
                    _deadPreyAmount.Value += 1;
                    break;
                default:
                    Debug.LogError($"{nameof(AnimalDeathCounterService)}.{nameof(AnimalDespawned)}(): "
                                   + $"animal type {animal.AnimalType} is not supported");
                    break;
            }
        }
    }
}
