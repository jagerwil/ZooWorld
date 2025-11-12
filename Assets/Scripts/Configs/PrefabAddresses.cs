using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ZooWorld.Configs {
    [CreateAssetMenu(fileName = "PrefabAddressesConfig", menuName = "Configs/Prefab Addresses")]
    public class PrefabAddresses : ScriptableObject {
        [field: SerializeField] public AssetReferenceGameObject PredatorTastyMessage { get; private set; }
    }
}
