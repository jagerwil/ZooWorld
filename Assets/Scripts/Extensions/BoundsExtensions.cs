using UnityEngine;

// ReSharper disable CheckNamespace
namespace ZooWorld {
    public static class BoundsExtensions {
        public static Vector3 GetRandomPoint(this Bounds bounds) {
            var min = bounds.min;
            var max = bounds.max;
            return new Vector3(Random.Range(min.x, max.x), 
                               Random.Range(min.y, max.y), 
                               Random.Range(min.z, max.z));
        }
    }
}
