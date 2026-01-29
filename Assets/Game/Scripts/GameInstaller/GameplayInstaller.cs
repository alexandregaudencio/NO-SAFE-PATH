using System.Collections.Generic;
using EnemyFactorySystem;
using Game.CharacterSystem;
using Game.PlayerSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] private EnemyController enemyPrefab;
    public override void InstallBindings()
    {
        Container.Bind<IPlayableCharacter>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInputHandler>().AsSingle().WithArguments(inputActions);
         Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
        
        // Container
        //     .BindFactory<EnemyController, EnemyFactory>()
        //     .FromComponentInNewPrefab(enemyPrefab)
        //     .UnderTransformGroup("Enemies");
        
        
        var prefabMap = new Dictionary<EnemyType, EnemyController>
        {
            { EnemyType.BEE, enemyPrefab },
            { EnemyType.SKULL, enemyPrefab },
            { EnemyType.ORC, enemyPrefab },
            { EnemyType.CARANGUEIJO, enemyPrefab },
        };
        
        Container.BindInstance(prefabMap);

        Container
            .BindFactory<EnemyType, EnemyController, EnemyFactory>()
            .FromMethod((container, type) =>
            {
                var map = container.Resolve<Dictionary<EnemyType, EnemyController>>();

                if (!map.TryGetValue(type, out var prefab))
                {
                    throw new System.Exception($"EnemyType não mapeado: {type}");
                }

                var enemy = container
                    .InstantiatePrefabForComponent<EnemyController>(prefab);

                // enemy.Initialize();
                return enemy;
            });
            
    }
    
  
}


