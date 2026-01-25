using Game.Attributes;
using UnityEngine;


namespace Game.CharacterSystem
{

    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private CharacterAttributes attributes;
        [SerializeField] private float jumpForce = 6f;
        private Rigidbody rigidbody;
        private CharacterMotor motor;

        private void Awake()
        {
            
            rigidbody = GetComponent<Rigidbody>();
            motor = new CharacterMotor(attributes, rigidbody);
        }


        public void Move(Vector2 direction)
        {
            motor.Move(direction );
        }

        public void Jump()
        {
            motor.Impulse(Vector3.up,jumpForce);
        }


    }
}
