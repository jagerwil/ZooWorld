using System;
using UnityEngine;
using Zenject;
using ZooWorld.Configs;

namespace ZooWorld.Architecture.Installers {
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Configs/Configs Installer", order = 0)]
    public class ConfigsInstaller : ScriptableObjectInstaller {
        [SerializeField] private ScenesAddressesConfig _scenesAddresses;
        [SerializeField] private PrefabAddresses _prefabAddresses;
        [SerializeField] private AnimalsConfig _animalsConfig;
        
        public override void InstallBindings() {
            Container.Bind<ScenesAddressesConfig>().FromInstance(_scenesAddresses).AsSingle();
            Container.Bind<PrefabAddresses>().FromInstance(_prefabAddresses).AsSingle();
            Container.Bind<AnimalsConfig>().FromInstance(_animalsConfig).AsSingle();
        }
    }
}
