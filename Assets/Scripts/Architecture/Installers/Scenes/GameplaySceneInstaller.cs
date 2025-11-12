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
using ZooWorld.Gameplay.Level;

namespace ZooWorld.Architecture.Installers.Scenes {
    public class GameplaySceneInstaller : MonoInstaller {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private AnimalSpawner _animalSpawner;
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
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
            
            Container.Bind<ILevelBoundsProvider>()
                     .To<LevelBoundsProvider>()
                     .AsSingle().WithArguments(_levelBounds);

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
