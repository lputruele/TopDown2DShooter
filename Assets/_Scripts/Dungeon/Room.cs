using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
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

    public List<Tuple<Vector2, Vector2>> doors = new List<Tuple<Vector2, Vector2>>();

    [SerializeField]
    private GameObject doorPrefab;

    [SerializeField]
    private GameObject torchPrefab;

    private bool isRespawning;

    // IDEA:
    // All rooms start with torches off, once entering a room, the doors close, once the room is clear, doors open and torches are on.

    private void Start()
    {
        /*
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
        }*/
    }

    private void Update()
    {
        if (!isRespawning)
            StartCoroutine(RespawnEnemiesCoroutine());       
    }

    IEnumerator RespawnEnemiesCoroutine()
    {
        isRespawning = true;
        yield return new WaitForSeconds(20);
        Enemies.RemoveAll((x) => x == null);
        if (Enemies.Count == 0 && EnemySpawner != null)
        {
            Cleared = true;
            EnemySpawner.StartSpawningAtSpawnPoint((Vector2)Center, this);
        }
        isRespawning = false;
    }

    public void SpawnEnemies()
    {
        EnemySpawner.StartSpawningAtSpawnPoint((Vector2)Center, this);
    }

    internal void DestroyRoom()
    {
        foreach (var enemy in Enemies)
        {
            Destroy(enemy);
        };
    }
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
