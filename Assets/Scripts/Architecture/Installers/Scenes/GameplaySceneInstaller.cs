using UnityEngine;
using Zenject;
using ZooWorld.Architecture.SceneInitializers;
using ZooWorld.Architecture.StateMachine.Gameplay;
using ZooWorld.Gameplay._Factories;
using ZooWorld.Gameplay._Factories.Implementations;
using ZooWorld.Gameplay._Providers;
using ZooWorld.Gameplay._Providers.Implementations;
using ZooWorld.Gameplay._Services;
using ZooWorld.Gameplay._Services.Implementations;
using ZooWorld.Gameplay.Animals.Spawners;

namespace ZooWorld.Architecture.Installers.Scenes {
    public class GameplaySceneInstaller : MonoInstaller {
        [SerializeField] private AnimalSpawner _animalSpawner;
        [SerializeField] private Camera _camera;
        [Space]
        [SerializeField] private Transform _animalsSpawnRoot;
        [SerializeField] private Transform _tastyMessagesSpawnRoot;
        
        public override void InstallBindings() {
            BindProviders();
            BindServices();
            BindFactories();
            
            BindGameStates();
            
            BindInitializer();
        }

        private void BindProviders() {
            Container.Bind<ICameraProvider>()
                     .To<CameraProvider>()
                     .AsSingle().WithArguments(_camera);
            
            Container.Bind<ILevelBoundsProvider>().To<LevelBoundsProvider>().AsSingle();

            Container.Bind<IAnimalSpawnerProvider>()
                     .To<AnimalSpawnerProvider>()
                     .AsSingle().WithArguments(_animalSpawner);
        }

        private void BindServices() {
            Container.Bind<IAnimalDeathCounterService>().To<AnimalDeathCounterService>().AsSingle();
        }

        private void BindFactories() {
            Container.Bind<IAnimalFactory>()
                     .To<AnimalFactory>()
                     .AsSingle().WithArguments(_animalsSpawnRoot);

            Container.Bind<IPredatorTastyMessageUIFactory>()
                     .To<PredatorTastyMessageUIFactory>()
                     .AsSingle().WithArguments(_tastyMessagesSpawnRoot);
        }

        private void BindGameStates() {
            Container.Bind<GameplayInitializationState>().AsSingle();
            Container.Bind<GameplayMainState>().AsSingle();
        }

        private void BindInitializer() {
            Container.BindInterfacesTo<GameplaySceneInitializer>().AsSingle();
        }
    }
}
