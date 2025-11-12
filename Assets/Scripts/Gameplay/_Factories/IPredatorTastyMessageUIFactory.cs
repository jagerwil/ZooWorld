using Cysharp.Threading.Tasks;
using Jagerwil.Core.Architecture.Factories;
using UnityEngine;
using ZooWorld.Gameplay.UI;

namespace ZooWorld.Gameplay._Factories {
    public interface IPredatorTastyMessageUIFactory : IGameFactory<PredatorTastyMessageUI> {
        public PredatorTastyMessageUI Spawn(Transform target);
    }
}
