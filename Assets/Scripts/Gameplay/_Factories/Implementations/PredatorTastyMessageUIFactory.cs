using Jagerwil.Core.Architecture.Factories.Implementations;
using Jagerwil.Core.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using ZooWorld.Configs;
using ZooWorld.Gameplay.UI;

namespace ZooWorld.Gameplay._Factories.Implementations {
    public class PredatorTastyMessageUIFactory : BaseGameFactory<PredatorTastyMessageUI>, IPredatorTastyMessageUIFactory {
        private readonly AssetReferenceGameObject _messageAddress;
        
        public PredatorTastyMessageUIFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            PrefabAddresses prefabAddresses,
            Transform defaultRoot)
            : base(instantiator, addressablesLoader, new MemoryPoolSettings(), defaultRoot) {
            _messageAddress = prefabAddresses.PredatorTastyMessage;
        }
        
        protected override AssetReferenceGameObject GetAssetReference() {
            return _messageAddress;
        }
        
        public PredatorTastyMessageUI Spawn(Transform target) {
            var message = CreateInternal();
            if (message) {
                message.Initialize(target);
            }
            return message;
        }
    }
}
