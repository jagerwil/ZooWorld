using Zenject;
using ZooWorld.Architecture.SceneInitializers;

namespace ZooWorld.Architecture.Installers.Scenes {
    public class EntryPointSceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindInterfacesTo<EntryPointSceneInitializer>().AsSingle();
        }
    }
}
