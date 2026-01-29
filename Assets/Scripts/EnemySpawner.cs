using Game.CharacterSystem;
using UnityEngine;
using Zenject;


namespace EnemyFactorySystem
{

    public class EnemySpawner : ITickable
    {

        [SerializeField] private int maximo = 50;
        [SerializeField] private EnemyController[] inimigos;
        [SerializeField] private float intervalo = 3;
        [SerializeField] private float raio = 3;
        private int contadorInimigos = 0;
       [Inject]  private EnemyFactory factory;
        

        private float tempo = 0;
        
        public void Tick()
        {
            Debug.Log("tick");
            tempo += Time.deltaTime;
            if (tempo >= intervalo)
            {
                tempo = 0;
                contadorInimigos++;
                var randomIndex = Random.Range(0, 3);
                Debug.Log(randomIndex + " index");
                
                var instance = factory.Create((EnemyType)randomIndex);
                
            }

        }

        // private void Start()
        // {
        //     StartCoroutine(GerarInimigos());
        // }
        //
        // private IEnumerator GerarInimigos()
        // {
        //     while (contadorInimigos < maximo)
        //     {
        //         GerarInimigo();
        //         contadorInimigos++;
        //         yield return new WaitForSeconds(intervalo);
        //     }
        // }
        //
        // private void GerarInimigo()
        // {
        //     int indiceAleatorio = Random.Range(0, inimigos.Length);
        //     var inimigoEscolhido = inimigos[indiceAleatorio];
        //     Instantiate(inimigoEscolhido, transform.position, Quaternion.identity);
        // }


    }
}
