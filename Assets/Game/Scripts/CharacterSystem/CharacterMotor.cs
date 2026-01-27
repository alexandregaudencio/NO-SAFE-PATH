using Game.Attributes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Game.CharacterSystem
{
    public class CharacterMotor
    {
        private readonly CharacterAttributes attributes;
        private readonly Transform transform;
        private readonly Rigidbody rigidbody;
        private readonly Transform feet;
        
        RaycastHit[] results = new RaycastHit[2];
        private static readonly LayerMask groundLayer = LayerMask.GetMask("Ground");
        
        public CharacterMotor(CharacterAttributes attributes, Rigidbody rigidbody, Transform feet)
        {
            this.attributes = attributes;
            this.transform = rigidbody.transform;
            this.rigidbody = rigidbody;
            this.feet = feet;
        }

        public void Move(Vector3 direction)
        {
            var newVelocity = direction.normalized * (attributes.speed * Time.fixedDeltaTime);
            newVelocity.y = rigidbody.linearVelocity.y;
            rigidbody.linearVelocity = newVelocity;
        }

        public void Impulse(Vector3 direction, float force)
        {
            rigidbody.AddForce(direction * force, ForceMode.Impulse);

        }


        public bool IsGrounded()
        {
            var cast = Physics.RaycastNonAlloc(feet.position, Vector3.down, results, 10,  groundLayer);
            return cast > 0;
        }
    }
}