using UnityEngine;


namespace Game.CharacterSystem
{

    public class CharacterController : MonoBehaviour
    {
        [Header("Config")] [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 6f;

        private Rigidbody rigidbody;
        private CharacterMotor motor;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            motor = new CharacterMotor(transform, rigidbody);
        }


        public void Run(Vector2 direction)
        {
            motor.Move(direction * moveSpeed);
        }

        public void Jump()
        {
            motor.Jump(jumpForce);
        }


    }
}
