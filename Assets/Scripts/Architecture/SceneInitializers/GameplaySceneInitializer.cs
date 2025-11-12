using System;
using Jagerwil.Core.Architecture.StateMachine;
using Zenject;
using ZooWorld.Architecture.StateMachine.Gameplay;

namespace ZooWorld.Architecture.SceneInitializers {
    public class GameplaySceneInitializer : IInitializable, IDisposable {
        private readonly IGameStateMachine _gameStateMachine;

        public GameplaySceneInitializer(IGameStateMachine gameStateMachine,
            GameplayInitializationState initializationState,
            GameplayMainState mainState) {
            _gameStateMachine = gameStateMachine;
            _gameStateMachine.Register(initializationState);
            _gameStateMachine.Register(mainState);
        }

        public void Dispose() {
            _gameStateMachine?.Unregister<GameplayInitializationState>();
            _gameStateMachine?.Unregister<GameplayMainState>();
        }

        public void Initialize() {
            _gameStateMachine.Enter<GameplayInitializationState>();
        }
    }
}
