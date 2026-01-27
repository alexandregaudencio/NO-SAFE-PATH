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
        
        private Ray ray;
        RaycastHit[] results = new RaycastHit[2];
        private LayerMask groundLayer = LayerMask.NameToLayer("Ground");
        public CharacterMotor(CharacterAttributes attributes, Rigidbody rigidbody, Transform feet)
        {
            ray = new Ray(feet.position, Vector3.down);
            this.attributes = attributes;
            this.transform = rigidbody.transform;
            this.rigidbody = rigidbody;
            this.feet = feet;
        }

        public void Move(Vector3 direction)
        {
            var newVelocity = direction.normalized * (attributes.speed * Time.fixedDeltaTime);
            //TODO: GC
            rigidbody.linearVelocity = new Vector3(newVelocity.x, rigidbody.linearVelocity.y, newVelocity.z);
        }

        public void Impulse(Vector3 direction, float force)
        {
            rigidbody.AddForce(direction * force, ForceMode.Impulse);

        }


        public bool IsGrounded()
        {
            ray.origin = feet.position;
            var cast = Physics.RaycastNonAlloc(feet.position, Vector3.down, results, 10);
            return cast > 0;
        }
    }
}