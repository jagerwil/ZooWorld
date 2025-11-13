using UnityEngine;

namespace ZooWorld.Gameplay._Providers.Implementations {
    public class CameraProvider : ICameraProvider {
        public Camera Camera { get; }
        
        public CameraProvider(Camera camera) {
            Camera = camera;
        }
    }
}
