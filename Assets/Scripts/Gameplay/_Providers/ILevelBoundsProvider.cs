using UnityEngine;

namespace ZooWorld.Gameplay._Providers {
    public interface ILevelBoundsProvider { 
        public Bounds LevelBounds { get; }
    }
}
