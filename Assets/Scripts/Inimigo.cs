using UnityEngine;
using Random = UnityEngine.Random;

public class Inimigo : Personagem
{
    protected override void Awake()
    {
        base.Awake();
        direcaoMovimento = Random.onUnitSphere.normalized;
        direcaoMovimento.y = 0;
    }

    private void Update()
    {
        AtualizaRotacao();
    }

    private void FixedUpdate()
    {
        Mover(direcaoMovimento);
    }

    protected override  void OnCollisionEnter(Collision collision)
    {
        AtualizaDirecaoMovimento(collision);
        base.OnCollisionEnter(collision);
    }
    
    private void AtualizaRotacao()
    {
        Vector3 direcaoRotation = direcaoMovimento;
        direcaoRotation.y = 0;  
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direcaoRotation), 30 * Time.deltaTime);
        
    }

    private void AtualizaDirecaoMovimento(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 normal = contact.normal;
            if (Mathf.Abs(normal.z) > Mathf.Abs(normal.x))
            {
                direcaoMovimento.z = -direcaoMovimento.z;
            }
            else
            {
                direcaoMovimento.x = -direcaoMovimento.x;
            }
            
        }
        direcaoMovimento = direcaoMovimento.normalized;

    }

}