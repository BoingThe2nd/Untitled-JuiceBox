using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{



    int SpawningEnemy;

    [SerializeField] float SpawnInterval;
    [SerializeField] int SpawnAmount;
    public List<GameObject> SpawnedEnemies = new List<GameObject>();

    public void SetSpawnerGuidelines(int SetEnemyType, float SetSpawnInterval, int SetNumberofSpawns )
    {
        SpawningEnemy = SetEnemyType;
        SpawnInterval = SetSpawnInterval;
        SpawnAmount = SetNumberofSpawns;

        StartCoroutine(BeginSpawningEnemies());
    }

    IEnumerator BeginSpawningEnemies()
    {

        int EnemySpawned = 0;

        while (EnemySpawned < SpawnAmount) 
        {

            GameObject newEnemy = Instantiate(GameManager.StaticEnemiesAvailable[SpawningEnemy], transform.position, Quaternion.identity);
            SpawnedEnemies.Add(newEnemy);
            newEnemy.GetComponent<Enemy_Base>().SetOwner(this);
            EnemySpawned += 1;
            yield return new WaitForSeconds(SpawnInterval);

            Debug.Log("Spawning Enemies");

        }

        while (SpawnedEnemies.Count > 0)
        {
            yield return new WaitForEndOfFrame();
            Debug.Log("Waiting For Enemies to Die");
        }


        Debug.Log("Enemies Have Cleared");
        GameManager.ActiveEnemySpawners.Remove(this.gameObject);
        Destroy(this.gameObject);

    }


}
