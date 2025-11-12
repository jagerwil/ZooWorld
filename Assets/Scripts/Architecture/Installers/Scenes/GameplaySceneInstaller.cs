using Zenject;
using ZooWorld.Architecture.SceneInitializers;

namespace ZooWorld.Architecture.Installers.Scenes {
    public class GameplaySceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindInterfacesTo<GameplaySceneInitializer>().AsSingle();
        }
    }
}
