using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.PlayerSystem
{

    public sealed class PlayerInputHandler : IDisposable
    {
        private const string ACTION_MAP = "Character";
        private const string MOVE_ACTION = "move";
        private const string JUMP_ACTION = "Jump"; 
        private InputActionAsset actionAsset;
        private InputActionMap actionMap;
        public InputAction MoveAction { get; private set; }
        public InputAction JumpAction { get; private set; }
        public static Vector3 direction = Vector3.zero;
        
        public PlayerInputHandler(InputActionAsset actionAsset)
        {
            this.actionAsset = actionAsset;
            MoveAction = actionAsset.FindAction(MOVE_ACTION);
            JumpAction = actionAsset.FindAction(JUMP_ACTION);
            actionMap =  actionAsset.FindActionMap(ACTION_MAP);
            actionMap.Enable();
        }
        
        public void Dispose()
        {
            actionMap.Disable();
        }

        public Vector3 MoveInput()
        {
            // direction.x = Input.GetAxis("Horizontal");
            // direction.y = Input.GetAxis("Vertical");
            var inputMoveValue = MoveAction.ReadValue<Vector2>();
            direction.x  = inputMoveValue.x;
            direction.z = inputMoveValue.y;
            return direction.normalized;
        }

        public bool JumpPressed()
        {
            // return Input.GetButtonDown(JUMP_ACTION);
            return JumpAction.WasPerformedThisFrame();
        }


    }
}