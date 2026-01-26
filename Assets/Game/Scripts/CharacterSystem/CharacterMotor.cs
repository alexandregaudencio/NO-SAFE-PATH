using Game.Attributes;
using UnityEngine;

namespace Game.CharacterSystem
{
    public class CharacterMotor
    {
        private readonly CharacterAttributes attributes;
        private readonly Transform transform;
        private readonly Rigidbody rigidbody;

        public CharacterMotor(CharacterAttributes attributes, Rigidbody rigidbody)
        {
            this.attributes = attributes;
            this.transform = rigidbody.transform;
            this.rigidbody = rigidbody;
        }

        public void Move(Vector3 direction)
        {
            var newVelocity = direction.normalized * (attributes.speed * Time.fixedDeltaTime);
            //TODO: GC
            rigidbody.linearVelocity = new Vector3(newVelocity.x, rigidbody.linearVelocity.y, newVelocity.z);
        }

        public void Impulse(Vector3 direction, float force)
        {
            rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);

        }

        private bool CheckGrounded()
        {
            return Physics.Raycast(
                transform.position,
                Vector3.down,
                1.1f
            );
        }
    }
}