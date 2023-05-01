using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [field:SerializeField]
    public EnemyGroupDataSO EnemyGroupData { get; set; }

    [SerializeField]
    private float minDelay = 0.8f, maxDelay = 1.5f;

    //[SerializeField]
    //private Dungeon dungeon;


    IEnumerator SpawnCoroutine(Room room)
    {
        if (room.EnemyCounts.Count == 0) // initialize enemy counts for each enemy in enemy group
        {
            for (int i = 0; i < EnemyGroupData.Enemies.Count; i++)
            {
                room.EnemyCounts.Add(Random.Range(EnemyGroupData.EnemyCountMin, EnemyGroupData.EnemyCountMax));
            }
        }

        Vector2Int[] roomFloor = room.Floor.ToArray();

        for (int i = 0; i < EnemyGroupData.Enemies.Count; i++) // populate room
        {         
            int roomEnemyCount = room.EnemyCounts[i];
            while (roomEnemyCount > 0)
            {
                roomEnemyCount--;
                /*bool validPosition = false;
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
                */                
                if (room.Enemies.Count < room.EnemyCapacity)
                {
                    Vector3 spawnPoint = (Vector2)roomFloor[Random.Range(0, roomFloor.Length)] + Vector2.right / 2 + Vector2.up / 2;
                    room.Enemies.Add(SpawnEnemy(EnemyGroupData.Enemies[i], spawnPoint, room.Cleared));
                }
                var randomTime = Random.Range(minDelay, maxDelay);
                yield return new WaitForSeconds(randomTime);
            }
        }            
    }

    /*
    private bool IsSpawnPointSafe(Vector3 spawnPoint)
    {
        bool isOnFloor = dungeon.Floor.Contains(Vector2Int.CeilToInt((Vector2)spawnPoint));
        isOnFloor &= dungeon.Floor.Contains(Vector2Int.FloorToInt((Vector2)spawnPoint));
        RaycastHit2D[] contacts = Physics2D.CircleCastAll((Vector2)spawnPoint, 1f, Vector2.right);
        
        float distanceToPlayer = Vector2.Distance(spawnPoint, dungeon.Player.transform.position);
        return isOnFloor && distanceToPlayer > 7f;
    }
*/
    private GameObject SpawnEnemy(GameObject enemy, Vector3 spawnPoint, bool isRespawn)
    {
        GameObject spawnedEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
        spawnedEnemy.GetComponent<Enemy>().IsRespawned = isRespawn;
        return spawnedEnemy;
    }

    public void StartSpawning(Room room)
    {
        StartCoroutine(SpawnCoroutine(room));
    }


}
