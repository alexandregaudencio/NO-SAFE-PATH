using System;
using Game.Attributes;
using UniRx;
using UnityEngine;

namespace Game.CharacterSystem
{
    public interface ICharacter : IDamageable
    {
        CharacterAttributes Attributes { get; }
        AnimationController AnimationController { get; }
        public ReactiveProperty<int> Health { get;  }
        public ReactiveProperty<CharacterState> State { get;  }
        void Move(Vector3 direction);
        void Jump();
        

    }
}