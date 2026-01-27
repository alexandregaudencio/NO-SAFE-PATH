using System;
using Game.Attributes;
using UnityEngine;

namespace Game.CharacterSystem
{
    public interface ICharacter : IDamageable
    {
        CharacterAttributes Attributes { get; }
        AnimationController AnimationController { get; }
        
        event Action<CharacterState> StateChanged;
        event Action<int> HealthChanged;
        void Move(Vector3 direction);
        void Jump();
        

    }
}