using Jagerwil.Core.Architecture.Factories.Implementations;
using Jagerwil.Core.Services;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;
using ZooWorld.Configs;
using ZooWorld.Gameplay.Animals;

namespace ZooWorld.Gameplay._Factories.Implementations {
    public class AnimalFactory : BaseGamePrefabFactory<Animal>, IAnimalFactory {
        private readonly AnimalsConfig _animalsConfig;

        public AnimalFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            AnimalsConfig animalsConfig,
            Transform defaultRoot)
            : base(instantiator, addressablesLoader, new MemoryPoolSettings(), defaultRoot) {
            _animalsConfig = animalsConfig;
        }
        
        [CanBeNull]
        public Animal Spawn(AnimalId id, Vector3 position) {
            var animalInfo = _animalsConfig.GetInfoById(id);
            return SpawnInternal(animalInfo, position);
        }

        [CanBeNull]
        public Animal SpawnRandom(Vector3 position) {
            var animalInfo = _animalsConfig.GetRandomInfo();
            return SpawnInternal(animalInfo, position);
        }

        [CanBeNull]
        private Animal SpawnInternal([CanBeNull] AnimalInfo info, Vector3 position) {
            if (info == null) {
                return null;
            }
            
            var animal = CreateInternal(info.Address, position, Quaternion.identity);
            return animal;
        }
    }
}
