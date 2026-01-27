using UnityEngine;
using Game.CharacterSystem;
using Zenject;

namespace Game.PlayerSystem
{

    public sealed class PlayerController : MonoBehaviour
    {
        private IPlayableCharacter playableCharacter;
        [Inject] private PlayerInputHandler inputHandler;

        [Inject]
        void Construct(IPlayableCharacter playableCharacter)
        {
            this.playableCharacter = playableCharacter;
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