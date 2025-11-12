using UnityEngine;
using Zenject;
using ZooWorld.Architecture.SceneInitializers;
using ZooWorld.Architecture.StateMachine.Gameplay;
using ZooWorld.Gameplay._Factories;
using ZooWorld.Gameplay._Factories.Implementations;
using ZooWorld.Gameplay._Providers;
using ZooWorld.Gameplay._Providers.Implementations;
using ZooWorld.Gameplay.Animals.Spawners;
using ZooWorld.Gameplay.Level;

namespace ZooWorld.Architecture.Installers.Scenes {
    public class GameplaySceneInstaller : MonoInstaller {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private AnimalSpawner _animalSpawner;
        [Space]
        [SerializeField] private Transform _animalsSpawnRoot;
        
        public override void InstallBindings() {
            BindProviders();
            BindFactories();
            BindGameStates();
            BindInitializer();
        }

        private void BindProviders() {
            Container.Bind<ILevelBoundsProvider>()
                     .To<LevelBoundsProvider>()
                     .AsSingle().WithArguments(_levelBounds);

            Container.Bind<IAnimalSpawnerProvider>()
                     .To<AnimalSpawnerProvider>()
                     .AsSingle().WithArguments(_animalSpawner);
        }

        private void BindFactories() {
            Container.Bind<IAnimalFactory>()
                     .To<AnimalFactory>()
                     .AsSingle().WithArguments(_animalsSpawnRoot);
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
