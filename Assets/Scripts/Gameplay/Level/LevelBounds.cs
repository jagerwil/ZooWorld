using UnityEngine;

namespace ZooWorld.Gameplay.Level {
    public class LevelBounds : MonoBehaviour {
        [SerializeField] private Vector2 _size;
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireCube(transform.position, new Vector3(_size.x, 1f, _size.y));
        }
#endif
        
        public Bounds GetBounds() {
            return new Bounds(transform.position, new Vector3(_size.x, float.MaxValue, _size.y));
        }
    }
}
