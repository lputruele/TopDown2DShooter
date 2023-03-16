using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameflowManager : MonoBehaviour
{
    [SerializeField]
    private int levelIndex;
    [SerializeField]
    private List<LevelDataSO> levels;
    [SerializeField]
    private List<AbstractDungeonGenerator> levelGenerators;
    [SerializeField]
    private Dungeon dungeon;
    [SerializeField]
    private AudioSource backgroundMusic;

    void Awake()
    {
        levelIndex = 0;
        ChangeDungeonData();
        ChangeEnemyGroups();
        ChangeMusic();
        dungeon.Generator = levelGenerators[levelIndex];
    }

    public void ChangeLevel()
    {
        levelIndex++;
        if (levelIndex == levels.Count - 1)
        {
            Destroy(dungeon.Exit);
        }
        ChangeDungeonData();
        ChangeEnemyGroups();
        ChangeMusic();
        dungeon.Generator = levelGenerators[levelIndex];
        dungeon.ResetDungeon();
    }

    private void ChangeEnemyGroups()
    {
        dungeon.bossPrefab = levels[levelIndex].LevelBoss;
        for (int i = 0; i < dungeon.EnemySpawners.Count; i++)
        {
            dungeon.EnemySpawners[i].EnemyGroupData = levels[levelIndex].LevelEnemyGroups[i];
        }
    }

    private void ChangeDungeonData()
    {
        dungeon.DungeonData = levels[levelIndex].DungeonData;
    }

    private void ChangeMusic()
    {
        backgroundMusic.clip = levels[levelIndex].LevelMusic;
        backgroundMusic.Play();
    }
}
