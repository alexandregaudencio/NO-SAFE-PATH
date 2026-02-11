using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CollectableSystem
{
    public class CollectableSpawner : ITickable
    {
        private const int MAX = 50;
        private readonly Bounds bounds;
        private float spawnInterval = 3;  //in seconds
        private int enemyCount = 0;
        private float releasedTime = 0;
        [Inject]  private CollectableFactory factory;

        public CollectableSpawner()
        {
            // this.bounds = bounds;
        }
    
        public void Tick()
        {
            releasedTime += Time.deltaTime;
            SpawnLoopUpdate();

        }

        public async void SpawnLoopUpdate()
        {
            if (releasedTime >= spawnInterval)
            {                 
                releasedTime = 0;
                if (enemyCount < MAX)
                {
                    enemyCount++;
                    await UniTask.Delay(1000);
                    int randomIndex = Random.Range(0, 2);
                    var instance = factory.Create((CollectableType)randomIndex);
                    var randomPosition =Random.insideUnitSphere * 10;
                    randomPosition.y = 1;
                    instance.transform.position = randomPosition;
                    
                }
           
            }
        }

       

    }
}