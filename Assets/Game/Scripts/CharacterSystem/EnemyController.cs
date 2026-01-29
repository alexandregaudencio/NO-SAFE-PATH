using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.CharacterSystem
{

    public class EnemyController : CharacterController
    {
        protected override void Awake()
        {
            base.Awake();
            moveDirection = Random.onUnitSphere.normalized;
            moveDirection.y = 0;
        }
        
        public void Initiliaze(Vector3 position)
        {
            transform.position = position;
        }

        private void Update()
        {
            UpdateRotation();
        }

        private void FixedUpdate()
        {
            Move(moveDirection);
        }

        protected override void OnCollisionEnter(Collision collision)
        {
            UpdateMoveDirection(collision);
            base.OnCollisionEnter(collision);
        }

        private void UpdateRotation()
        {
            Vector3 direcaoRotation = moveDirection;
            direcaoRotation.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direcaoRotation),
                30 * Time.deltaTime);

        }

        private void UpdateMoveDirection(Collision collision)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Vector3 normal = contact.normal;
                if (Mathf.Abs(normal.z) > Mathf.Abs(normal.x))
                {
                    moveDirection.z = -moveDirection.z;
                }
                else
                {
                    moveDirection.x = -moveDirection.x;
                }

            }

            moveDirection = moveDirection.normalized;

        }

    }
}