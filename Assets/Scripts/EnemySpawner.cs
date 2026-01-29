using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.CharacterSystem;
using UnityEngine;
using Zenject;


namespace EnemyFactorySystem
{

    public class EnemySpawner : ITickable
    {

        private const int MAX = 50;
        // private List<EnemyController> enemies = new();
        private float spawnInterval = 3;  //in seconds
        private int enemyCount = 0;
        private SpawnHole[] holes;
        private float releasedTime = 0;
       [Inject]  private EnemyFactory factory;

        public EnemySpawner(SpawnHole[] holes)
        {
            this.holes = holes;
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
                    var hole = holes[Random.Range(0, holes.Length)];
                    hole.ActiveFeedback();
                    await UniTask.Delay(1000);
                    var enemyRandomIndex = Random.Range(0, 3);
                    var enemyInstance = factory.Create((EnemyType)enemyRandomIndex);
                    var positionRandomIndex = hole.Position;
                    enemyInstance.Initiliaze(positionRandomIndex);
                    // enemies.Add(enemyInstance);
                }
               
            }
        }

       

    }
}
