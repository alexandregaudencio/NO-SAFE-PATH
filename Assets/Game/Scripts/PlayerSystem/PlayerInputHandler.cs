using UnityEngine;

namespace Game.PlayerSystem
{

    public sealed class PlayerInputHandler
    {
        public Vector2 ReadMovement()
        {
            return new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ).normalized;
        }

        public bool JumpPressed()
        {
            return Input.GetButtonDown("Jump");
        }
    }
}