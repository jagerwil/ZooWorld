using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZooWorld.Gameplay._Providers;

namespace ZooWorld.Gameplay.UI {
    public class PredatorTastyMessageUI : MonoBehaviour, IPoolable {
        [SerializeField] private float _labelAppearDuration = 1f;
        [SerializeField] private float _predatorHeight = 2f;
        
        [Inject] private ICameraProvider _cameraProvider;
        
        private Transform _target;
        private CancellationTokenSource _cancelToken;

        public void Initialize(Transform target) {
            _target = target;
        }

        private void OnDestroy() {
            _cancelToken?.Dispose();
        }

        private void Update() {
            UpdateScreenPosition();
        }

        public void Show() {
            gameObject.SetActive(true);
            UpdateScreenPosition();
            
            HideAfterDelay().Forget();
        }

        public void OnSpawned() { }

        public void OnDespawned() {
            _cancelToken?.Cancel();
            gameObject.SetActive(false);
        }

        private void UpdateScreenPosition() {
            var screenPos = _cameraProvider.Camera.WorldToScreenPoint(_target.position);

            var highestPoint = _target.position + new Vector3(0f, _predatorHeight, 0f);
            var highestScreenPos = _cameraProvider.Camera.WorldToScreenPoint(highestPoint);

            if (highestScreenPos.y < screenPos.y) {
                screenPos.y = highestScreenPos.y;
            }
            transform.position = screenPos;
        }

        private async UniTask HideAfterDelay() {
            _cancelToken?.Cancel();
            _cancelToken = new CancellationTokenSource();
            
            await UniTask.WaitForSeconds(_labelAppearDuration, cancellationToken: _cancelToken.Token);
            gameObject.SetActive(false);
        }
    }
}
