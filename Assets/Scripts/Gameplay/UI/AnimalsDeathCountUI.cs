using System;
using R3;
using TMPro;
using UnityEngine;
using Zenject;
using ZooWorld.Gameplay._Services;

namespace ZooWorld.Gameplay.UI {
    public class AnimalsDeathCountUI : MonoBehaviour {
        [SerializeField] private TMP_Text _predatorsDeathAmountText;
        [SerializeField] private TMP_Text _preyDeathAmountText;
        
        private readonly CompositeDisposable _disposables = new();

        [Inject]
        private void Inject(IAnimalDeathCounterService deathCounterService) {
            deathCounterService.DeadPredatorsAmount
                               .Subscribe(DeadPredatorsAmountChanged)
                               .AddTo(_disposables);
            
            deathCounterService.DeadPreyAmount
                               .Subscribe(DeadPreyAmountChanged)
                               .AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables?.Dispose();
        }

        private void DeadPredatorsAmountChanged(int predatorsAmount) {
            _predatorsDeathAmountText.text = predatorsAmount.ToString();
        }

        private void DeadPreyAmountChanged(int preyAmount) {
            _preyDeathAmountText.text = preyAmount.ToString();
        }
    }
}
