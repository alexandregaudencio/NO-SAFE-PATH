using System.Collections;
using Game.CharacterSystem;
using UnityEngine;

public class GeradorDeInimigos : MonoBehaviour
{

    [SerializeField] private int maximo = 50;
    [SerializeField] private EnemyController[] inimigos;
    [SerializeField] private float intervalo = 3;
    [SerializeField] private float raio = 3;
    private int contadorInimigos = 0;
    private void Start()
    {
        StartCoroutine(GerarInimigos());
    }

    private IEnumerator GerarInimigos()
    {
        while (contadorInimigos < maximo)
        {
            GerarInimigo();
            contadorInimigos++;
            yield return new WaitForSeconds(intervalo);
        }
    }

    private void GerarInimigo()
    {
        int indiceAleatorio = Random.Range(0, inimigos.Length);
        var inimigoEscolhido = inimigos[indiceAleatorio];
        Instantiate(inimigoEscolhido, transform.position, Quaternion.identity);
    }

}
