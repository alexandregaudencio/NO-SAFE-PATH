using System.Collections.Generic;
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
        private Vector3[] holePositions;
        private float releasedTime = 0;
       [Inject]  private EnemyFactory factory;

        public EnemySpawner(Vector3[] holePositions)
        {
            this.holePositions = holePositions;
        }
        
        public void Tick()
        {
            releasedTime += Time.deltaTime;
            SpawnLoopUpdate();

        }

        public void SpawnLoopUpdate()
        {
            if (releasedTime >= spawnInterval)
            {                 
                releasedTime = 0;
                if (enemyCount < MAX)
                {
                    enemyCount++;
                    var enemyRandomIndex = Random.Range(0, 3);
                    var enemyInstance = factory.Create((EnemyType)enemyRandomIndex);
                    var positionRandomIndex = holePositions[Random.Range(0, holePositions.Length)];
                    enemyInstance.Initiliaze(positionRandomIndex);
                    // enemies.Add(enemyInstance);
                }
               
            }
        }

       

    }
}
