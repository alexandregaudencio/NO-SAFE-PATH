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
    [SerializeField] private EnemyCatalogSO enemyCatalog;
    [SerializeField] private CollectableCatalogSO collectableCatalog;
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
        var enemyCatalogDic = enemyCatalog.EnemyDictionary as Dictionary<EnemyType, EnemyController>;
        Container.BindInstance(enemyCatalogDic);

      
        Container
            .BindFactory<EnemyType, EnemyController, EnemyFactory>()
            //ver depois o que faz
            .FromMethod((container, type) =>
            {   
                //Resolve: Find enemyCatalogSO
                var enemyCatalogDicMapped = container.Resolve<Dictionary<EnemyType, EnemyController>>();
        
                if (!enemyCatalogDicMapped.TryGetValue(type, out var prefab))
                {
                    throw new System.Exception($"EnemyType out of map: {type}");
                }
        
                var enemy = container
                    .InstantiatePrefabForComponent<EnemyController>(prefab);
        
                // enemy.Initialize();
                return enemy;
            });


        var collectableDictionary = collectableCatalog.GetcollectableDictionary();
        Container.BindInstance(collectableDictionary);

        
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


