using System;
using System.Collections.Generic;
using System.Linq;
using Jagerwil.Core.Utils.Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ZooWorld.Gameplay.Animals;

namespace ZooWorld.Configs {
    [CreateAssetMenu(fileName = "AnimalsConfig", menuName = "Configs/Animals")]
    public class AnimalsConfig : ScriptableObject {
        [SerializeField] private List<AnimalInfo> _animals;
        
        private LookupTable<AnimalId, AnimalInfo> _animalsLookup;

        [CanBeNull]
        public AnimalInfo GetInfoById(AnimalId id) {
            if (_animalsLookup == null) {
                _animalsLookup = new LookupTable<AnimalId, AnimalInfo>(_animals, animal => animal.Id);
            }

            return _animalsLookup.GetElement(id);
        }

        [CanBeNull]
        public AnimalInfo GetRandomInfo() {
            if (_animals.Count == 0) {
                Debug.LogError($"{nameof(AnimalsConfig)}.{nameof(GetRandomInfo)}(): Animals list is empty!");
                return null;
            }
            return _animals[UnityEngine.Random.Range(0, _animals.Count)];
        }

        public IReadOnlyList<AssetReferenceGameObject> GetAllAddresses() {
            return _animals.Select(animal => animal.Address).ToList();
        }
    }

    [Serializable]
    public class AnimalInfo {
        [field: SerializeField] public AnimalId Id { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Address { get; private set; }
    }
}
