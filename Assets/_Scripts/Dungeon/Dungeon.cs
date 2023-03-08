using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [field: SerializeField]
    public AbstractDungeonGenerator Generator { get; set; }

    [field:SerializeField]
    public List<Room> Rooms { get; set; }

    [SerializeField]
    private List<EnemySpawner> enemySpawners;

    [SerializeField]
    private GameObject playerPrefab; // this should be on a game manager

    private void Awake()
    {
        Generator.GenerateDungeon();
        Room playerRoom = Rooms[Random.Range(0, Rooms.Count)];
        playerRoom.RoomType = RoomType.Player;
        playerPrefab.transform.position = (Vector3)(Vector2)playerRoom.Center;
        foreach (var room in Rooms)
        {
            if (room.RoomType != RoomType.Player)
            {
                room.RoomType = RoomType.Enemy;
                int index = Random.Range(0, enemySpawners.Count);
                enemySpawners[index].SpawnPoints.Add((Vector3)(Vector2)room.Center);
            }

        }
    }

    private void Start()
    {
        
    }
}
