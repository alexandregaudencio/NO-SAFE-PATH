using UnityEngine;
using Game.CharacterSystem;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.PlayerSystem
{

    public sealed class PlayerController : MonoBehaviour
    {
        private IPlayableCharacter playableCharacter;
        private PlayerInputHandler inputHandler;

        [Inject]
        void Construct(IPlayableCharacter playableCharacter, PlayerInputHandler inputHandler)
        {
            this.playableCharacter = playableCharacter;
            this.inputHandler = inputHandler;
        }

        private void OnEnable()
        {
            inputHandler.JumpAction.started += OnJumpInputPerformed;
        }

        private void OnDisable()
        {            
            inputHandler.JumpAction.started -= OnJumpInputPerformed;
        }
        


        private void OnJumpInputPerformed(InputAction.CallbackContext obj)
        {
                playableCharacter.Jump();


        }
        

        private void FixedUpdate()
        {
            playableCharacter.Move(inputHandler.MoveInput());
            
            
        }


    }
}