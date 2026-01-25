using System;
using Game.Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Personagem : MonoBehaviour, IDanificavel
{
    protected Rigidbody rigidBody;
    [FormerlySerializedAs("Atributos")] [SerializeField] protected CharacterAttributes characterAttributes;
    protected Vector3 direcaoMovimento = new Vector3(1, 0, 1);

    public UnityEvent Morreu;
    public event Action<int> VidaMudou;
    private int vidaAtual;

    public void MudarVida(int valor)
    {
        vidaAtual = Mathf.Clamp(vidaAtual - valor, 0, characterAttributes.vida);
        VidaMudou?.Invoke(vidaAtual);
    }
    private void OnEnable()
    {
        vidaAtual = characterAttributes.vida;
        Morreu.AddListener(DesativarObjeto);
    }
    private void OnDisable()
    {
        Morreu.RemoveListener(DesativarObjeto);
    }

    private void DesativarObjeto()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag(gameObject.tag))
        {
            if (other.gameObject.TryGetComponent(out IDanificavel danificavel))
            {
                danificavel.AplicarDano(characterAttributes.dano);
            }
        }
    }



    public void AplicarDano(int dano)
    {
        MudarVida(dano);
        if (vidaAtual <= 0)
        {
            Morreu.Invoke();
        }
    }

    protected void Mover(Vector3 direcao)
    {
        Vector3 velocidadeH = direcao.normalized * (characterAttributes.velocidade * Time.fixedDeltaTime);
        rigidBody.linearVelocity = new Vector3(velocidadeH.x, rigidBody.linearVelocity.y, velocidadeH.z);
    }


}
