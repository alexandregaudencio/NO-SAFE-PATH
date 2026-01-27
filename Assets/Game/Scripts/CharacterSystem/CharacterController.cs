using System;
using Game.Attributes;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;


namespace Game.CharacterSystem
{
    //PlayableCharacterState
    public enum CharacterState
    {
        Idle,
        Walk, 
        Jump,
        Death
    }

  

    // public class CharacterModel : ICharacter
    // {
    //     public void ApplyDamage(int damage)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public CharacterAttributes Attributes { get; }
    //     public AnimationController AnimationController { get; }
    //     public event Action<CharacterState> StateChanged;
    //     public event Action<int> HealthChanged;
    //     public void Move(Vector3 direction)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     public void Jump()
    //     {
    //         throw new NotImplementedException();
    //     }
    // }
    
    public class CharacterController : MonoBehaviour, ICharacter
    {
        [field: SerializeField] public CharacterAttributes Attributes { get; private set; }
        [SerializeField] private float jumpForce = 6f;
        private int currentHealth;
        protected Vector3 moveDirection;
        protected Rigidbody rigidbody;
        protected CharacterMotor motor;
        protected Animator animator;
        public AnimationController AnimationController { get; private set; }

        public CharacterState CurrentState { get;private  set; } = CharacterState.Idle;
        // private ObservableCollection<string> observableCollection = new();
        public event Action<CharacterState> StateChanged; 
        public event Action<int> HealthChanged;
        
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            motor = new CharacterMotor(Attributes, rigidbody);
            animator = GetComponentInChildren<Animator>();
            SetState(CharacterState.Idle);
            currentHealth = Attributes.health;
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
        }    
        
        protected void SetState(CharacterState state) 
        {
            CurrentState = state;
            StateChanged?.Invoke(CurrentState);
            
        }

        public void Move(Vector3 direction)
        { 
            moveDirection = direction;
            motor.Move(moveDirection);
            if (CurrentState != CharacterState.Walk)
            {
                SetState(CharacterState.Walk);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection),20*Time.fixedDeltaTime);

        }

        public void Jump()
        {
            if(CurrentState == CharacterState.Jump) return;
            else SetState(CharacterState.Jump);
            motor.Impulse(Vector3.up,jumpForce);
            AnimationController.Play("Jump");

        }
        
        
        protected virtual void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag(gameObject.tag))
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(Attributes.damage);
                }
            }
        }

        private void SetHealth(int valor)
        {
            currentHealth = Mathf.Clamp(currentHealth - valor, 0, Attributes.health);
            HealthChanged?.Invoke(currentHealth);
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
