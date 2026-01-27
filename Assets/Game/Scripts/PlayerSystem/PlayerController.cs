using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Game.CharacterSystem;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using CharacterController = Game.CharacterSystem.CharacterController;

namespace Game.PlayerSystem
{

    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;
        [SerializeField] private PlayableCharacter playableCharacter;
        private PlayerInputHandler inputHandler;
        private void Awake()
        {
            inputHandler = new(inputActionAsset);
        }
        
        
        private void FixedUpdate()
        {
            
            playableCharacter.Move(inputHandler.MoveInput());
            
        }

        private void Update()
        {
            if (inputHandler.JumpPressed())
            {
                playableCharacter.Jump();
            }
        }
    }
}