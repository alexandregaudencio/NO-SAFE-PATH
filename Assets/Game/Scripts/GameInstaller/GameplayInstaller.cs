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
        
        // Container.BindFactory<EnemyController, EnemyFactory>();
        Container
            .BindFactory<EnemyController, EnemyFactory>()
            .FromComponentInNewPrefab(enemyPrefab)
            .UnderTransformGroup("Enemies");
        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
        // Container.BindInterfacesTo<EnemySpawner>().AsSingle();
    }
    
  
}
