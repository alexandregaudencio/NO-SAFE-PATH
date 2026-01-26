using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Game.CharacterSystem;
using UnityEngine.InputSystem;
using CharacterController = Game.CharacterSystem.CharacterController;

namespace Game.PlayerSystem
{

    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActionAsset;

        private PlayerInputHandler inputHandler;
        [SerializeField] private CharacterController characterController;
        private void Awake()
        {
            inputHandler = new(inputActionAsset);
        }
        
        private void FixedUpdate()
        {
            
            characterController.Move(inputHandler.MoveInput());
            
        }

        private void Update()
        {
            if (inputHandler.JumpPressed())
            {
                characterController.Jump();
            }
        }
    }
}