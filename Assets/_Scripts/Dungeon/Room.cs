using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    [field: SerializeField]
    public HashSet<Vector2Int> Floor { get; set; } = new HashSet<Vector2Int>();
    [SerializeField]
    public Vector2Int Center { get; set; } = new Vector2Int();

    [field:SerializeField]
    public RoomType RoomType { get; set; } = RoomType.None;

    [field: SerializeField]
    public EnemySpawner EnemySpawner { get; set; }

    public bool Cleared { get; set; }
    public List<GameObject> Enemies { get; set; } = new List<GameObject>();

    [field: SerializeField]
    public List<int> EnemyCounts { get; set; } = new List<int>();

    [field: SerializeField]
    public int EnemyCapacity { get; set; }

    public List<Tuple<Vector2, Vector2>> doors = new List<Tuple<Vector2, Vector2>>();

    [SerializeField]
    private GameObject doorPrefab;

    [SerializeField]
    private GameObject torchPrefab;

    private bool isRespawning;

    [SerializeField]
    public Dungeon Dungeon { get; set; }

    // IDEA:
    // All rooms start with torches off, once entering a room, the doors close, once the room is clear, doors open and torches are on.

    /*private void Start()
    {
        
        foreach (var door in doors)
        {
            if (door.Item1.x == door.Item2.x)
            {
                GameObject newDoor = Instantiate(doorPrefab, new Vector2(door.Item1.x, door.Item1.y + (door.Item2.y - door.Item1.y)/2), Quaternion.identity);
                newDoor.transform.localScale = new Vector3(door.Item2.x - door.Item1.x + 0.1f, door.Item2.y - door.Item1.y + 0.1f, 0);
            }
            else
            {
                GameObject newDoor = Instantiate(doorPrefab, new Vector2(door.Item1.x + (door.Item2.x - door.Item1.x) / 2, door.Item1.y), Quaternion.identity);
                newDoor.transform.localScale = new Vector3(door.Item2.x - door.Item1.x + 0.1f, door.Item2.y - door.Item1.y + 0.1f, 0);
            }
        }
}
    */
    /*private void Update()
    {
        if (!isRespawning)
            StartCoroutine(RespawnEnemiesCoroutine());       
    }*/

    IEnumerator RespawnEnemiesCoroutine()
    {
        isRespawning = true;
        yield return new WaitForSeconds(30);
        Enemies.RemoveAll((x) => x == null);
        if (Enemies.Count == 0 && EnemySpawner != null)
        {
            Cleared = true;
            float distanceToPlayer = Vector2.Distance(Center, Dungeon.Player.transform.position);
            if (distanceToPlayer > 12f)
                EnemySpawner.StartSpawning(this);
        }
        isRespawning = false;
    }

    public void SpawnEnemies()
    {
        EnemySpawner.StartSpawning(this);
    }

    /*internal void DestroyRoom()
    {
        foreach (var enemy in Enemies)
        {
            Destroy(enemy);
        };
    }*/
}

public enum RoomType
{
    None,
    Start,
    Enemy,
    Treasure,
    Boss,
    Exit
}
