using UnityEngine;
using ZooWorld.Gameplay.Level;

namespace ZooWorld.Gameplay._Providers.Implementations {
    public class LevelBoundsProvider : ILevelBoundsProvider {
        public Bounds LevelBounds { get; }

        public LevelBoundsProvider(LevelBounds levelBounds) {
            LevelBounds = levelBounds.GetBounds();
        }
    }
}
