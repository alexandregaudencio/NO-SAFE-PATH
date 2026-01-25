using UnityEngine;

namespace Game.PlayerSystem
{

    public sealed class PlayerInputHandler
    {
        public static Vector2 direction;
        public Vector2 MoveInput()
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");
            return direction.normalized;
        }

        public bool JumpPressed()
        {
            return Input.GetButtonDown("Jump");
        }
    }
}