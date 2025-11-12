using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using ZooWorld.Gameplay._Factories;
using ZooWorld.Gameplay.Animals;

namespace ZooWorld.Gameplay.UI {
    public class PredatorTastyMessagesHolderUI : MonoBehaviour {
        private IAnimalFactory _animalFactory;
        private IPredatorTastyMessageUIFactory _messageFactory;
        
        private Dictionary<Animal, PredatorTastyMessageUI> _messages = new();
        private HashSet<Animal> _spawnList = new();

        [Inject]
        private void Inject(IAnimalFactory animalFactory, IPredatorTastyMessageUIFactory messageFactory) {
            _animalFactory = animalFactory;
            _messageFactory = messageFactory;
            
            animalFactory.OnSpawned += AnimalSpawned;
            animalFactory.OnDespawned += AnimalDespawned;
        }

        private void OnDestroy() {
            if (_animalFactory != null) {
                _animalFactory.OnSpawned -= AnimalSpawned;
                _animalFactory.OnDespawned -= AnimalDespawned;
            }
        }

        private void ShowAllMessages() {
            foreach (var animalToSpawn in _spawnList) {
                SpawnMessage(animalToSpawn);
            }
            _spawnList.Clear();
            
            foreach (var messagePair in _messages) {
                messagePair.Value.Show();
            }
        }

        private void AnimalSpawned(Animal animal) {
            if (animal.AnimalType == AnimalType.Predator) {
                _spawnList.Add(animal);
            }
        }

        private void AnimalDespawned(Animal animal) {
            if (animal.AnimalType == AnimalType.Predator) {
                DespawnMessage(animal);
            }
            
            ShowAllMessages();
        }

        private void SpawnMessage(Animal animal) {
            if (_messages.ContainsKey(animal)) {
                Debug.LogWarning($"Message was already added for animal {animal}");
                return;
            }
            
            var message = _messageFactory.Spawn(animal.transform);
            _messages.Add(animal, message);
        }

        private void DespawnMessage(Animal animal) {
            _spawnList.Remove(animal);
            
            if (_messages.TryGetValue(animal, out var message)) {
                _messageFactory.Despawn(message);
                _messages.Remove(animal);
            }
        }
    }
}
