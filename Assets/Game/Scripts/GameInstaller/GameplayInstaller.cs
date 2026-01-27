using System;
using Game.CharacterSystem;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.Bind<IPlayableCharacter>().FromComponentInHierarchy().AsSingle();
        

    }
    
  
}
