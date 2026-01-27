using UnityEngine;
using Game.CharacterSystem;
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