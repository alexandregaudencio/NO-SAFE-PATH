using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using CollectableSystem;
using EnemyFactorySystem;
using Game.CharacterSystem;
using Game.PlayerSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] InputActionAsset inputActions;
    // [SerializeField] private EnemyController enemyPrefab;
    [SerializedDictionary] public SerializedDictionary<EnemyType, EnemyController> enemies;
    [SerializedDictionary] public SerializedDictionary<CollectableType, CollectableObject> collectables;
    [SerializeField] private SpawnHole[] holes;
   
    public override void InstallBindings()
    {
        Container.Bind<IPlayableCharacter>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInputHandler>().AsSingle().WithArguments(inputActions);

        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle()
            .WithArguments(holes);
        Container.BindInterfacesAndSelfTo<CollectableSpawner>().AsSingle();
        
        // Container
        //     .BindFactory<EnemyController, EnemyFactory>()
        //     .FromComponentInNewPrefab(enemyPrefab)
        //     .UnderTransformGroup("Enemies");


        var prefabMap = enemies as Dictionary<EnemyType, EnemyController>;
        
        Container.BindInstance(prefabMap);

        Container
            .BindFactory<EnemyType, EnemyController, EnemyFactory>()
            .FromMethod((container, type) =>
            {
                var map = container.Resolve<Dictionary<EnemyType, EnemyController>>();

                if (!map.TryGetValue(type, out var prefab))
                {
                    throw new System.Exception($"EnemyType out of map: {type}");
                }

                var enemy = container
                    .InstantiatePrefabForComponent<EnemyController>(prefab);

                // enemy.Initialize();
                return enemy;
            });
        
        Container
            .BindFactory<CollectableType, CollectableObject, CollectableFactory>()
            .FromMethod((container, type) =>
            {
                var map = container.Resolve<Dictionary<CollectableType, CollectableObject>>();

                if (!map.TryGetValue(type, out var prefab))
                {
                    throw new System.Exception($"Collectable type out of map: {type}");
                }

                var collectable = container
                    .InstantiatePrefabForComponent<CollectableObject>(prefab);
                return collectable;
            });
            
    }
    
  
}


