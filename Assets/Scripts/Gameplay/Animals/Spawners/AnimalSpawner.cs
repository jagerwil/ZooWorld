using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZooWorld.Gameplay._Factories;
using ZooWorld.Gameplay._Providers;

namespace ZooWorld.Gameplay.Animals.Spawners {
    public class AnimalSpawner : MonoBehaviour {
        [SerializeField] private int _initialAnimalsAmount = 10;
        [SerializeField] private float _spawnInterval = 1f;
        
        [Inject] private ILevelBoundsProvider _levelBoundsProvider;
        [Inject] private IAnimalFactory _animalFactory;

        private CancellationTokenSource _cancelToken;

        private void OnDestroy() {
            _cancelToken?.Dispose();
        }

        public void StartSpawning() {
            for (int i = 0; i < _initialAnimalsAmount; i++) {
                SpawnAnimal();
            }
            SpawnAnimalsEndlesslyAsync().Forget();
        }

        public void StopSpawning() {
            _cancelToken?.Cancel();
        }

        private async UniTask SpawnAnimalsEndlesslyAsync() {
            _cancelToken = new CancellationTokenSource();
            while (true) {
                await UniTask.WaitForSeconds(_spawnInterval, cancellationToken: _cancelToken.Token);
                SpawnAnimal();
            }
        }

        private void SpawnAnimal() {
            var spawnPoint = _levelBoundsProvider.LevelBounds.GetRandomPoint();
            spawnPoint.y = 0f;
            _animalFactory.SpawnRandom(spawnPoint);
        }
    }
}
