using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Jogador : Personagem
{   
    [SerializeField] private Material material;
    [SerializeField] Color ColorInicial;
    [SerializeField] private float alturaPulo = 20;
    public Animator animatior;
    private AudioSource audioSource;
    private bool pulando = false;
    [SerializeField] private AudioClip puloAudioClip;
    [SerializeField] private AudioClip hitAudioClip;
    protected override void Awake()
    {
        base.Awake();
        audioSource =  GetComponent<AudioSource>();
        material.color = ColorInicial;
    }

    private void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");
        direcaoMovimento = new Vector3(xInput, 0, zInput);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direcaoMovimento),20*Time.deltaTime);
        
        animatior.SetFloat("speedXZ", Mathf.Abs(direcaoMovimento.magnitude));

        if (Input.GetKeyDown(KeyCode.Space) && pulando == false)
        {
            pulando = true;
            rigidBody.AddForce(Vector3.up*alturaPulo,ForceMode.Impulse);
            animatior.SetBool("pulando", true);
            PlayAudioClip(puloAudioClip);
        }
    }

    private void FixedUpdate()
    {
        Mover(direcaoMovimento);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayAudioClip(hitAudioClip);
            StartCoroutine(EfeitoDano());
        }
        if (other.gameObject.CompareTag("Solo"))
        {
            pulando = false;
            animatior.SetBool("pulando", false);
        }
    }



    public IEnumerator EfeitoDano()
    {
        material.color = Color.softRed;
        yield return new WaitForSeconds(0.2f);
        material.color = ColorInicial;
    }

    private void PlayAudioClip(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}