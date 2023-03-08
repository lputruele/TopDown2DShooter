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

    IEnumerator SpawnCoroutine()
    {
        while (enemyCount > 0)
        {
            enemyCount--;
            var randomIndex = Random.Range(0, SpawnPoints.Count);
            bool validPosition = false;
            Vector2 randomOffset;
            Vector3 spawnPoint = Vector3.zero;
            int tries = 0;
            while (!validPosition && tries < 10)
            {
                tries++;
                randomOffset = Random.insideUnitCircle * spawnRadius;
                spawnPoint = SpawnPoints[randomIndex] + (Vector3)randomOffset;
                validPosition = IsSpawnPointSafe(spawnPoint);
            }
            if (validPosition)
                SpawnEnemy(spawnPoint);
            var randomTime = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomTime);
        }
        
    }

    private bool IsSpawnPointSafe(Vector3 spawnPoint)
    {
        //todo:Check that spawnpoint is on floor
        int contacts = Physics2D.CircleCastAll((Vector2)spawnPoint, enemyPrefab.GetComponent<Enemy>().SafeSpawnRadius, Vector2.right).Length;
        return contacts == 1;
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    private void Start()
    {
        if (SpawnPoints.Count > 0)
        {
            Debug.Log("aaa");
            /*foreach (var spawnPoint in spawnPoints)
            {
                SpawnEnemy(spawnPoint.transform.position);
            }*/
            StartCoroutine(SpawnCoroutine());
        }
    }
}
