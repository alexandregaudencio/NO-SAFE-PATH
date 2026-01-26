using System;
using Game.Attributes;
using UnityEditor.Animations;
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
        protected AnimationController animationController;
        
        // private ObservableCollection<string> observableCollection = new();
        public CharacterState currentState = CharacterState.Idle;
        public event Action<CharacterState> OnStateChange; 
        public event Action<int> HealthChange;
        
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            motor = new CharacterMotor(attributes, rigidbody);
            animator = GetComponentInChildren<Animator>();
            SetState(CharacterState.Idle);
            currentHealth = attributes.health;
            animationController = new AnimationController(GetComponentInChildren<Animator>());
        }    
        
        protected void SetState(CharacterState state) 
        {
            currentState = state;
            OnStateChange?.Invoke(currentState);
            
        }

        public virtual void Move(Vector3 direction)
        { 
            moveDirection = direction;
            if (currentState != CharacterState.Walk)
            {
                SetState(CharacterState.Walk);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection),20*Time.deltaTime);

        }

        public void Jump()
        {
            if(currentState == CharacterState.Jump) return;
            else SetState(CharacterState.Jump);
            motor.Impulse(Vector3.up,jumpForce);
            animationController.Play("Jump");

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
