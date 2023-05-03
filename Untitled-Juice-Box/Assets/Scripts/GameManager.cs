using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public static GameManager TheGameManager;
    public enum GameState { Defend, Build}
    public static GameState m_GameState;
    public GameState StartState;
    [SerializeField] float TimeBeforeLevel;

    [Header("Item Data")]
    [SerializeField] int CurrentItemPool = 0;
    [System.Serializable]
    public struct ItemPool { public int MinLevel; public List<GameObject> ItemsInPool; }
    public List<ItemPool> TheItemPool = new List<ItemPool>();
    public GameObject[] ItemSpawners;
    public Transform[] ItemSpawnerLocations;

    List<GameObject> AvailableItems = new List<GameObject>();

    [Header("Spawner Data")]
    public static int Level = 0;

    [SerializeField] GameObject EnemySpawners;
    [SerializeField] Transform[] EnemySpawnerSpawnLocations;
    public float SpawnerSpawnDelay;
    public float EnemySpawnDelay;

    public static List<GameObject> ActiveEnemySpawners = new List<GameObject>();

    [Header("Enemy Pool Data")]
    [SerializeField] int CurrentEnemyPool = 0;
    [System.Serializable]
    public struct EnemyPool { public int MinLevel; public List<GameObject> EnemiesInPool;} 
    public List<EnemyPool> TheEnemyPool = new List<EnemyPool>();

    public static GameObject[] StaticEnemiesAvailable;

  
    public enum Flavour { Neutral, Apple, Orange, Grape }
    // Start is called before the first frame update
    void Start()
    {
        TheGameManager = this;
        m_GameState = StartState;
        StaticEnemiesAvailable = TheEnemyPool[CurrentEnemyPool].EnemiesInPool.ToArray();
    }

    public void BeginNextLevel()
    {
        if (m_GameState != GameState.Defend)
        {
            m_GameState = GameState.Defend;
            StartCoroutine(Defending());
            StartCoroutine(CreateSpawners());
        }
    }

    IEnumerator CreateSpawners()
    {

        float spawnerWaitTime = SpawnerSpawnDelay - TimeBeforeLevel;
        int amountofSpawnersSpawned = 0;
        int numberToSpawn = (Level / 4) + 1;
        while (amountofSpawnersSpawned < numberToSpawn)
        {

            spawnerWaitTime += Time.deltaTime;
            if (spawnerWaitTime >= SpawnerSpawnDelay)
            {
                GameObject newSpawner = Instantiate(EnemySpawners, EnemySpawnerSpawnLocations[Random.Range(0, EnemySpawnerSpawnLocations.Length)]);
                ActiveEnemySpawners.Add(newSpawner);
                #region //LevelMath
                //Level 12 (9 + 0 + 5) 4 Spawners * 14  == 56
                //Level 11 (6 + 6 + 5) 3 Spawners * 17  == 51
                //Level 10 (6 + 4 + 5) 3 Spawners * 15  == 45
                //Level 9 (6 + 2 + 5) 3 Spawners * 13  == 39
                //Level 8 (6 + 0 + 5) 3 Spawners * 11  == 33
                //Level 7 (3 + 6 + 5) 2 Spawners * 14  == 28
                //Level 6 (3 + 4 + 5) 2 Spawners * 12  == 24
                //Level 5 (3 + 2 + 5) 2 Spawners * 10 == 20
                //Level 4 (3 + 0 + 5) 2 Spawners * 8 == 16 
                //Level 3 (0 + 6 + 5) 1 Spawners * 11 == 11
                //Level 2 (0 + 4 + 5) 1 Spawners * 9 == 9
                //level 1 (0 + 2 + 5) 1 Spawner * 7 == 7
                #endregion
                newSpawner.GetComponent<Enemy_Spawner>().SetSpawnerGuidelines(Random.Range(0,StaticEnemiesAvailable.Length), EnemySpawnDelay,(Level/4 *3) + ((Level % 4) * 2) + 5);
                amountofSpawnersSpawned += 1;
                spawnerWaitTime = 0;

                Debug.Log("Created Spawner");
            }

            Debug.Log("Creating Spawners");
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Defending()
    {
        yield return new WaitForSeconds(TimeBeforeLevel);

        StartCoroutine(CreateSpawners());

        while (ActiveEnemySpawners.Count > 0)
        {
            Debug.Log("Waiting for Spawners To Clear");
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(TimeBeforeLevel);

        Debug.Log("Back to Build");
        m_GameState = GameState.Build;
        ProgressLevel();
    } 

    public static void ProgressLevel()
    {
        Level += 1;
        //Change Enemy Pool
        if (TheGameManager.CurrentEnemyPool < TheGameManager.TheEnemyPool.Count - 1)
        {
            if (Level >= TheGameManager.TheEnemyPool[TheGameManager.CurrentEnemyPool + 1].MinLevel)
            {
                TheGameManager.CurrentEnemyPool += 1;
                StaticEnemiesAvailable = TheGameManager.TheEnemyPool[TheGameManager.CurrentEnemyPool].EnemiesInPool.ToArray();
            }
        }
        //Creating New Item Bases
        if (TheGameManager.CurrentItemPool < TheGameManager.TheItemPool.Count - 1)
        {
            if (Level >= TheGameManager.TheItemPool[TheGameManager.CurrentItemPool + 1].MinLevel)
            {
                TheGameManager.CurrentItemPool += 1;
                for (int i = 0; i < TheGameManager.TheItemPool[TheGameManager.CurrentItemPool].ItemsInPool.Count; i++)
                {
                    TheGameManager.AvailableItems.Add(TheGameManager.TheItemPool[TheGameManager.CurrentItemPool].ItemsInPool[i]);
                }
                
                //StaticEnemiesAvailable = TheGameManager.TheItemPool[TheGameManager.CurrentItemPool].ItemsInPool.ToArray();
            }
        }
        //Add New Fruits
        Farm.TheFarm.GenerateFarmFruits();

    }

}
