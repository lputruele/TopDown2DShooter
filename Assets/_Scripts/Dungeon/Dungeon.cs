using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Dungeon : MonoBehaviour
{
    [field: SerializeField]
    public DungeonDataSO DungeonData { get; set; }

    [field: SerializeField]
    public AbstractDungeonGenerator Generator { get; set; }

    [field:SerializeField]
    public List<Room> Rooms { get; set; } // assume at least 3 rooms

    [field: SerializeField]
    public HashSet<Vector2Int> Floor { get; set; } = new HashSet<Vector2Int>();

    [field: SerializeField]
    public HashSet<Vector2Int> TrapPositions { get; set; } = new HashSet<Vector2Int>();

    [field:SerializeField]
    public List<EnemySpawner> EnemySpawners { get; set; }

    [field: SerializeField]
    public GameObject Player { get; set; } // this should probably be on a game manager

    [field: SerializeField]
    public GameObject Exit { get; set; }

    

    [SerializeField]
    public GameObject bossPrefab;
    [SerializeField]
    private GameObject treasureChestPrefab;
    [SerializeField]
    private GameObject candlePrefab;
    [SerializeField]
    private GameObject trapPrefab;

    [SerializeField]
    private ItemDropper treasureRoomItemDropper;


    private HashSet<int> usedRoomIndexes = new HashSet<int>();

    [field: SerializeField]
    public UnityEvent OnResetDungeon{ get; set; }

    private void Awake()
    {
        Player = FindObjectOfType<Player>().gameObject;
        Generator.GenerateDungeon();        
        PlaceTraps();        
    }

    private void Start()
    {
        InitializePlayerRoom();
        InitializeBossRoom();
        InitializeExitRoom();
        InitializeTreasureRooms();
        InitializeMonsterRooms();
    }

    private void PlaceTraps()
    {
        foreach (var position in Floor)
        {
            bool isInAnyRoom = false;
            float placementChance = DungeonData.TrapPlacementChance;
            foreach (var room in Rooms)
            {
                if (room.Floor.Contains(position))
                {
                    isInAnyRoom = true;
                    break;
                }
            }
            if (!isInAnyRoom) //higher chance of trap in corridors
                placementChance *= 10;
            if (Random.Range(0f, 1f) < placementChance)
            {
                Instantiate(trapPrefab, (Vector2)position + Vector2.up/2 + Vector2.right/2, Quaternion.identity);
            }
                
        }
    }

    private void InitializePlayerRoom()
    {
        // Set up player room  
        Room playerRoom = Rooms[AssignRoom()];
        playerRoom.RoomType = RoomType.Start;
        Player.transform.position = (Vector2)playerRoom.Center;
    }

    private void InitializeExitRoom()
    {
        // Set up exit room
        if (Exit != null)
        {
            Room exitRoom = Rooms[AssignRoom()];
            exitRoom.RoomType = RoomType.Exit;
            Exit.transform.position = (Vector2)exitRoom.Center;
        }
    }

    private void InitializeBossRoom()
    {        
        // Set up boss room
        Room bossRoom = Rooms[AssignRoom()];
        bossRoom.RoomType = RoomType.Boss;
        Instantiate(bossPrefab, (Vector2)bossRoom.Center, Quaternion.identity);
    }

    private void InitializeMonsterRooms()
    {
        // Set up enemies rooms
        foreach (var room in Rooms)
        {
            if (room.RoomType == RoomType.None)
            {
                room.RoomType = RoomType.Enemy;
                int index = Random.Range(0, EnemySpawners.Count);
                room.EnemySpawner = EnemySpawners[index];
                room.SpawnEnemies();
            }

        }
    }

    private void InitializeTreasureRooms()
    {
        Room treasureRoom = Rooms[AssignRoom()];
        treasureRoom.RoomType = RoomType.Treasure;
        treasureRoomItemDropper.DropItemAtPosition((Vector2)treasureRoom.Center);
        Instantiate(candlePrefab, (Vector2)treasureRoom.Center + Vector2.left, Quaternion.identity);
        Instantiate(candlePrefab, (Vector2)treasureRoom.Center + Vector2.right, Quaternion.identity);
        foreach (var room in Rooms)
        {
            if (room.RoomType == RoomType.None)
            {
                float placementChance = .1f;
                if (Random.Range(0f, 1f) < placementChance)
                {
                    Instantiate(treasureChestPrefab, (Vector2)room.Center, Quaternion.identity);
                }
            }

        }
    }

    private int AssignRoom()
    {
        Assert.AreNotEqual(usedRoomIndexes.Count, Rooms.Count); // this prevents a possibly infinite loop
        int index = Random.Range(0, Rooms.Count);
        while (usedRoomIndexes.Contains(index))
        {
            index = Random.Range(0, Rooms.Count);
        }
        usedRoomIndexes.Add(index);
        return index;
    }

    public void ResetDungeon()
    {
        DestroyLevel();
        OnResetDungeon?.Invoke();
        Generator.GenerateDungeon();

        InitializePlayerRoom();
        InitializeBossRoom();
        InitializeExitRoom();
        InitializeTreasureRooms();
        InitializeMonsterRooms();
        PlaceTraps();
    }

    public void DestroyLevel()
    {
        foreach (Item droppedItem in FindObjectsOfType<Item>())
        {
            Destroy(droppedItem.gameObject);
        }
        foreach (TreasureChest chest in FindObjectsOfType<TreasureChest>())
        {
            Destroy(chest.gameObject);
        }
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            Destroy(enemy.gameObject);
        }
        foreach (Room room in FindObjectsOfType<Room>())
        {
            Destroy(room.gameObject);
        }
        foreach (Trap trap in FindObjectsOfType<Trap>())
        {
            Destroy(trap.gameObject);
        }
        foreach (GameObject decoration in GameObject.FindGameObjectsWithTag("Decoration"))
        {
            Destroy(decoration);
        }
        /*foreach (var room in Rooms)
        {
            //room.DestroyRoom();
            Destroy(room.gameObject);
        }*/
        usedRoomIndexes.Clear();
    }
}
