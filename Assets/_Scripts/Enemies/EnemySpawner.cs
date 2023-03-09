using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [field:SerializeField]
    public List<Vector3> SpawnPoints { get; set; } = new List<Vector3>();
    //public List<Vector3> spawnPoints;

    [SerializeField]
    private int enemyCount = 20;

    [SerializeField]
    private float minDelay = 0.8f, maxDelay = 1.5f, spawnRadius = 3f;

    [SerializeField]
    private Dungeon dungeon;

    IEnumerator SpawnCoroutine()
    {
        foreach (var spawnPoint in SpawnPoints)
        {
            int roomEnemyCount = enemyCount;
            while (roomEnemyCount > 0)
            {
                roomEnemyCount--;
                //var randomIndex = Random.Range(0, SpawnPoints.Count);
                bool validPosition = false;
                Vector2 randomOffset;
                Vector3 offsettedSpawnPoint = Vector3.zero;
                int tries = 0;
                while (!validPosition && tries < 10)
                {
                    tries++;
                    randomOffset = Random.insideUnitCircle * spawnRadius;
                    offsettedSpawnPoint = spawnPoint + (Vector3)randomOffset;
                    validPosition = IsSpawnPointSafe(offsettedSpawnPoint);
                }
                if (validPosition)
                    SpawnEnemy(offsettedSpawnPoint);
                var randomTime = Random.Range(minDelay, maxDelay);
                yield return new WaitForSeconds(randomTime);
            }
        }    
    }

    private bool IsSpawnPointSafe(Vector3 spawnPoint)
    {
        bool isOnFloor = dungeon.Floor.Contains(Vector2Int.RoundToInt((Vector2)spawnPoint));
        int contacts = Physics2D.CircleCastAll((Vector2)spawnPoint, enemyPrefab.GetComponent<Enemy>().SafeSpawnRadius, Vector2.right).Length;
        return contacts == 1 && isOnFloor;
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    public void StartSpawning()
    {
        if (SpawnPoints.Count > 0)
        {
            StartCoroutine(SpawnCoroutine());
        }
    }
}
