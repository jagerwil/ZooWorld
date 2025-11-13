using UnityEngine;

namespace ZooWorld.Gameplay._Providers.Implementations {
    public class LevelBoundsProvider : ILevelBoundsProvider {
        public Bounds LevelBounds { get; }

        public LevelBoundsProvider(ICameraProvider cameraProvider) {
            LevelBounds = cameraProvider.Camera.GetWorldBoundsRaycast(new Vector3(0.5f, -0.5f, 0.5f));
        }
    }
}
