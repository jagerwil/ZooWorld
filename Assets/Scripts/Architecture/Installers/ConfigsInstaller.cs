using UnityEngine;
using Zenject;
using ZooWorld.Configs;

namespace ZooWorld.Architecture.Installers {
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Configs/Configs Installer", order = 0)]
    public class ConfigsInstaller : ScriptableObjectInstaller {
        [SerializeField] private ScenesAddressesConfig _scenesAddresses;
        
        public override void InstallBindings() {
            Container.Bind<ScenesAddressesConfig>().FromInstance(_scenesAddresses).AsSingle();
        }
    }
}
