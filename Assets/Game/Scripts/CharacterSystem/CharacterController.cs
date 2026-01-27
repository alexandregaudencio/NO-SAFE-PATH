using System;
using Game.Attributes;
using UniRx;
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
        protected Vector3 moveDirection;
        protected Rigidbody rigidbody;
        protected CharacterMotor motor;
        protected Animator animator;
        public AnimationController AnimationController { get; private set; }
        public ReactiveProperty<int> Health { get; private set; }
        public ReactiveProperty<CharacterState> State { get; private set; } = new(CharacterState.Idle);
        // private ObservableCollection<string> observableCollection = new();
        
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            motor = new CharacterMotor(Attributes, rigidbody);
            animator = GetComponentInChildren<Animator>();
            State.Value = (CharacterState.Idle);
            Health  = new(Attributes.health);
            AnimationController = new AnimationController(GetComponentInChildren<Animator>());
        }    
        


        public void Move(Vector3 direction)
        { 
            moveDirection = direction;
            motor.Move(moveDirection);
            if (State.Value != CharacterState.Walk)
            {
                State.Value =(CharacterState.Walk);
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection),20*Time.fixedDeltaTime);

        }

        public void Jump()
        {
            if(State.Value == CharacterState.Jump) return;
            else State.Value = CharacterState.Jump;
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
            Health.Value = Mathf.Clamp(Health.Value - valor, 0, Attributes.health);
        }


        public void ApplyDamage(int damage)
        {
            SetHealth(damage);
            if (Health.Value <= 0)
            {
                State.Value = (CharacterState.Death);
                this.gameObject.SetActive(false);

            }
        }
  
    }
}
