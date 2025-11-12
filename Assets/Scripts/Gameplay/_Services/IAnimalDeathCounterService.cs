using R3;

namespace ZooWorld.Gameplay._Services {
    public interface IAnimalDeathCounterService {
        public ReadOnlyReactiveProperty<int> DeadPredatorsAmount { get; }
        public ReadOnlyReactiveProperty<int> DeadPreyAmount { get; }

        public void StartCounting();
    }
}
