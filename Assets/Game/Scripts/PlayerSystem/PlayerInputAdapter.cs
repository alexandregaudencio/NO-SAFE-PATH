using UnityEngine.InputSystem;

namespace Game.Input
{
    public class UnityInputAdapter : IPlayerInput
    {
        private readonly PlayerInputActions _actions;

        public UnityInputAdapter(PlayerInputActions actions)
        {
            _actions = actions;
            _actions.Player.Enable();
        }

        public float Move =>
            _actions.Player.Move.ReadValue<float>();

        public bool JumpPressed =>
            _actions.Player.Jump.WasPressedThisFrame();
    }
}
