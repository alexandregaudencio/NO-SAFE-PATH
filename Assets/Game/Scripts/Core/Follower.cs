using UnityEngine;


namespace Game.Core
{

    public class Follower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 initialPosition;

        private void Awake()
        {
            initialPosition = (transform.position - target.position);
        }

        private void Update()
        {
            transform.position = target.position + initialPosition;

        }


    }
}
