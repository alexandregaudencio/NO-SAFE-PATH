using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class Personagem : MonoBehaviour, IDanificavel
{
    protected Rigidbody rigidBody;
    [SerializeField] protected Atributos Atributos;
    protected Vector3 direcaoMovimento = new Vector3(1, 0, 1);

    public UnityEvent Morreu;
    public event Action<int> VidaMudou;
    private int vidaAtual;

    public void MudarVida(int valor)
    {
        vidaAtual = Mathf.Clamp(vidaAtual - valor, 0, Atributos.vida);
        VidaMudou?.Invoke(vidaAtual);
    }
    private void OnEnable()
    {
        vidaAtual = Atributos.vida;
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
                danificavel.AplicarDano(Atributos.dano);
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
        Vector3 velocidadeH = direcao.normalized * (Atributos.velocidade * Time.fixedDeltaTime);
        rigidBody.linearVelocity = new Vector3(velocidadeH.x, rigidBody.linearVelocity.y, velocidadeH.z);
    }


}
