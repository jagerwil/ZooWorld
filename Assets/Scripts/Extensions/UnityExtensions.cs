using UnityEngine;

// ReSharper disable CheckNamespace
namespace ZooWorld {
    public static class UnityExtensions {
        public static Bounds GetWorldBoundsRaycast(this Camera camera, Vector3 padding) {
            var minPointRay = camera.ScreenPointToRay(new Vector3(0f, 0f, 0f));
            var maxPointRay = camera.ScreenPointToRay(new Vector3(Screen.width - 1f, Screen.height - 1f, 0f));

            Vector3 minPoint;
            if (Physics.Raycast(minPointRay, out var minHit)) {
                minPoint = minHit.point;
            }
            else {
                Debug.LogError("Cannot get camera bounds for bottom left corner!");
                return new Bounds();
            }

            Vector3 maxPoint;
            if (Physics.Raycast(maxPointRay, out var maxHit)) {
                maxPoint = maxHit.point;
            }
            else {
                Debug.LogError("Cannot get camera bounds for top right corner!");
                return new Bounds();
            }

            var center = (minPoint + maxPoint) * 0.5f;
            var size = (maxPoint - minPoint) - 2f * padding;

            return new Bounds(center, size);
        }
        
        public static Vector3 GetRandomPoint(this Bounds bounds) {
            var min = bounds.min;
            var max = bounds.max;
            return new Vector3(Random.Range(min.x, max.x), 
                               Random.Range(min.y, max.y), 
                               Random.Range(min.z, max.z));
        }
    }
}
