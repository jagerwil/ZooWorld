using Cysharp.Threading.Tasks;
using Jagerwil.Core.Architecture.StateMachine;
using ZooWorld.Configs;
using ZooWorld.Gameplay._Factories;

namespace ZooWorld.Architecture.StateMachine.Gameplay {
    public class GameplayInitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IAnimalFactory _animalFactory;
        private readonly IPredatorTastyMessageUIFactory _tastyMessageFactory;
        private readonly AnimalsConfig _animalConfig;

        public GameplayInitializationState(IGameStateMachine stateMachine,
            IAnimalFactory animalFactory,
            IPredatorTastyMessageUIFactory tastyMessageFactory,
            AnimalsConfig animalsConfig) {
            _stateMachine = stateMachine;
            _animalFactory = animalFactory;
            _tastyMessageFactory = tastyMessageFactory;
            _animalConfig = animalsConfig;
        }
        
        public void Enter() {
            WarmUpFactoriesAsync().Forget();
        }
        
        public void Exit() { }

        private async UniTask WarmUpFactoriesAsync() {
            var animalsTask = _animalFactory.WarmUpAsync(_animalConfig.GetAllAddresses());
            var tastyMessageTask = _tastyMessageFactory.WarmUpAsync();
            
            await UniTask.WhenAll(animalsTask, tastyMessageTask);
            
            _stateMachine.Enter<GameplayMainState>();
        }
    }
}
