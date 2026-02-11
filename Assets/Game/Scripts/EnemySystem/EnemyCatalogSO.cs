using System;
using AYellowpaper.SerializedCollections;
using Game.CharacterSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace EnemyFactorySystem
{
    [CreateAssetMenu(menuName = "enemy catalog")]
    public class EnemyCatalogSO : ScriptableObject
    {
        [field: SerializeField] public SerializedDictionary<EnemyType, EnemyController> EnemyDictionary { get; private set; }

        
    }
}
