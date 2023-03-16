using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

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
    private GameObject treasurePrefab;
    [SerializeField]
    private GameObject trapPrefab;


    private HashSet<int> usedRoomIndexes = new HashSet<int>();

    [field: SerializeField]
    public UnityEvent OnResetDungeon{ get; set; }

    private void Awake()
    {
        Player = FindObjectOfType<Player>().gameObject;
        Generator.GenerateDungeon();
        
        InitializePlayerRoom();
        InitializeBossRoom();
        InitializeExitRoom();
        InitializeTreasureRooms();        
        InitializeMonsterRooms();
        PlaceTraps();

        
    }

    private void Start()
    {
        /*foreach (var room in Rooms)
        {
            room.SpawnEnemies();
        }*/
    }

    private void PlaceTraps()
    {
        foreach (var position in Floor)
        {
            if (!Floor.Contains(position + Vector2Int.down))
                continue;
            if (!Floor.Contains(position + Vector2Int.down + Vector2Int.left))
                continue;
            if (!Floor.Contains(position + Vector2Int.left))
                continue;
            if (!Floor.Contains(position + Vector2Int.up + Vector2Int.left))
                continue;

            if (!Floor.Contains(position + Vector2Int.up))
                continue;
            if (!Floor.Contains(position + Vector2Int.up + Vector2Int.right))
                continue;
            if (!Floor.Contains(position + Vector2Int.right))
                continue;
            if (!Floor.Contains(position + Vector2Int.down + Vector2Int.right))
                continue;
            bool isInAnyRoom = false;
            float placementChance = .02f;
            foreach (var room in Rooms)
            {
                if (room.Floor.Contains(position))
                {
                    isInAnyRoom = true;
                    break;
                }
            }
            if (!isInAnyRoom) //higher chance of trap in corridors
                placementChance *= 20;
            if (Random.Range(0f, 1f) < placementChance)
            {
                int random = Random.Range(0, 4);
                Vector2 offset = random == 0? Vector2.right: random == 1 ? Vector2.left: random == 2 ? Vector2.up : Vector2.down;
                Instantiate(trapPrefab, (Vector2)position + offset/2, Quaternion.identity);
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
        Instantiate(treasurePrefab, (Vector2)treasureRoom.Center, Quaternion.identity);
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
        /*foreach (var room in Rooms)
        {
            //room.DestroyRoom();
            Destroy(room.gameObject);
        }*/
        usedRoomIndexes.Clear();
    }
}
