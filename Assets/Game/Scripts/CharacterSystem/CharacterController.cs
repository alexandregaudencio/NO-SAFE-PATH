using System;
using Game.Attributes;
using UnityEngine;


namespace Game.CharacterSystem
{
    public enum CharacterState
    {
        Idle,
        Walk, 
        Jump,
        Death
    }
    
    public class CharacterController : MonoBehaviour, IDamageable
    {
        [SerializeField] private CharacterAttributes attributes;
        [SerializeField] private float jumpForce = 6f;
        private int currentHealth;
        protected Vector3 moveDirection;
        protected Rigidbody rigidbody;
        protected CharacterMotor motor;
        protected Animator animator;
        
        // private ObservableCollection<string> observableCollection = new();
        public CharacterState currentState = CharacterState.Idle;
        public event Action<CharacterState> OnStateChange; 
        public event Action<int> HealthChange;
        
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            motor = new CharacterMotor(attributes, rigidbody);
            animator = GetComponent<Animator>();
            SetState(CharacterState.Idle);
            currentHealth = attributes.health;
        }    
        
        protected void SetState(CharacterState state) 
        {
            currentState = state;
            OnStateChange?.Invoke(currentState);
            
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Move(Vector3 direction)
        {
            moveDirection = direction;
            if (currentState != CharacterState.Walk)
            {
                SetState(CharacterState.Walk);
            }
            motor.Move(direction );
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection),20*Time.deltaTime);

        }

        public void Jump()
        {
            if(currentState == CharacterState.Jump) return;
            else SetState(CharacterState.Jump);
            motor.Impulse(Vector3.up,jumpForce);
            animator.SetBool("pulando", true);

        }
        
        
        protected virtual void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(gameObject.tag))
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(attributes.damage);
                }
            }
        }

        private void SetHealth(int valor)
        {
            currentHealth = Mathf.Clamp(currentHealth - valor, 0, attributes.health);
            HealthChange?.Invoke(currentHealth);
        }


        public void ApplyDamage(int damage)
        {
            SetHealth(damage);
            if (currentHealth <= 0)
            {
                SetState(CharacterState.Death);
                this.gameObject.SetActive(false);

            }
        }
  
    }
}
