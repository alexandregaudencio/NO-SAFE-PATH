using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.PlayerSystem
{

    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerController : MonoBehaviour
    {
        private PlayerInputHandler inputHandler = new();
        [SerializeField] private Game.CharacterSystem.CharacterController characterController;


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