using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field:SerializeField]
    public EnemyGroupDataSO EnemyGroupData { get; set; }

    //[field:SerializeField]
    public List<Vector3> SpawnPoints { get; set; } = new List<Vector3>();
    //public List<Vector3> spawnPoints;

    [SerializeField]
    private float minDelay = 0.8f, maxDelay = 1.5f, spawnRadius = 3f;

    [SerializeField]
    private Dungeon dungeon;

    IEnumerator SpawnCoroutine(Vector3 spawnPoint, Room room)
    {
        for (int i = 0; i < EnemyGroupData.Enemies.Count; i++)
        {
            int roomEnemyCount = EnemyGroupData.EnemyCounts[i];
            while (roomEnemyCount > 0)
            {
                roomEnemyCount--;
                bool validPosition = false;
                Vector2 randomOffset;
                Vector3 offsettedSpawnPoint = Vector3.zero;
                int tries = 0;
                while (!validPosition && tries < 3)
                {
                    tries++;
                    randomOffset = Random.insideUnitCircle * spawnRadius;
                    offsettedSpawnPoint = spawnPoint + (Vector3)randomOffset;
                    validPosition = IsSpawnPointSafe(offsettedSpawnPoint);
                }
                if (validPosition)
                    room.Enemies.Add(SpawnEnemy(EnemyGroupData.Enemies[i], offsettedSpawnPoint, room.Cleared));
                var randomTime = Random.Range(minDelay, maxDelay);
                yield return new WaitForSeconds(randomTime);
            }
        }            
    }

    private bool IsSpawnPointSafe(Vector3 spawnPoint)
    {
        bool isOnFloor = dungeon.Floor.Contains(Vector2Int.CeilToInt((Vector2)spawnPoint));
        isOnFloor &= dungeon.Floor.Contains(Vector2Int.FloorToInt((Vector2)spawnPoint));
        //int contacts = Physics2D.CircleCastAll((Vector2)spawnPoint, enemy.GetComponent<Enemy>().SafeSpawnRadius, Vector2.right).Length;
        float distanceToPlayer = Vector2.Distance(spawnPoint, dungeon.Player.transform.position);
        return isOnFloor && distanceToPlayer > 5f;
    }

    private GameObject SpawnEnemy(GameObject enemy, Vector3 spawnPoint, bool isRespawn)
    {
        GameObject spawnedEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        spawnedEnemy.GetComponent<Enemy>().IsRespawned = isRespawn;
        return spawnedEnemy;
    }

    public void StartSpawning(Room room)
    {
        if (SpawnPoints.Count > 0)
        {
            foreach (var spawnPoint in SpawnPoints)
            {
                StartCoroutine(SpawnCoroutine(spawnPoint, room));
            }            
        }
    }

    public void StartSpawningAtSpawnPoint(Vector3 spawnPoint, Room room)
    {
        StartCoroutine(SpawnCoroutine(spawnPoint, room));
    }


}
