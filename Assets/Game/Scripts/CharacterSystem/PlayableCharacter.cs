using System;
using System.Collections;
using Game.CharacterSystem;
using UnityEngine;

namespace Game.CharacterSystem
{



    public class PlayableCharacter : CharacterController, IPlayableCharacter
    {        
        private static readonly int XZspeed = Animator.StringToHash("XZspeed");
        [SerializeField] private Material material;
        [SerializeField] Color ColorInicial;
        [SerializeField] private float alturaPulo = 20;
        private AudioSource audioSource;
        [SerializeField] private AudioClip puloAudioClip;
        [SerializeField] private AudioClip hitAudioClip;

        protected override void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
            material.color = ColorInicial;
        }

          protected void Update()
          {
              animator.SetFloat(XZspeed, Mathf.Abs( moveDirection.normalized.magnitude));

          }
        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);
            if (other.gameObject.CompareTag("Enemy"))
            {
                PlayAudioClip(hitAudioClip);
                StartCoroutine(DamageEffect());
            }

            if (other.gameObject.CompareTag("Solo"))
            {
                State.Value =(CharacterState.Idle);
                animator.SetBool("pulando", false);
            }
        }

        protected IEnumerator DamageEffect()
        {
            material.color = Color.softRed;
            yield return new WaitForSeconds(0.2f);
            material.color = ColorInicial;
        }

        protected void PlayAudioClip(AudioClip audioClip)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}