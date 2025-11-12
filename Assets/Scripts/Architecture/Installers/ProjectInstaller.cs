using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using Jagerwil.Core.Services.Implementations;
using Zenject;
using ZooWorld.Architecture._Services;
using ZooWorld.Architecture._Services.Implementations;
using ZooWorld.Architecture.StateMachine;

namespace ZooWorld.Architecture.Installers {
    public class ProjectInstaller : MonoInstaller {
        public override void InstallBindings() {
            BindServices();
            BindGameStateMachine();
        }

        private void BindServices() {
            Container.Bind<IAddressablesLoader>().To<AddressablesLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }

        private void BindGameStateMachine() {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<SceneLoadingState>().AsSingle();
        }
    }
}
