using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public int maxEnemies = 1000;
    public static GameController controller;

    private List<GameObject> pooledObjects;

    public void Start()
    {
        controller = this;
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject temp = Instantiate(enemyPrefab);
            temp.GetComponent<EnemyBehaviour>().player = playerPrefab;
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public void SpawnEnemies()
    {
        int numSpawns = Random.Range(1, 3);
        for (int i = 0; i < numSpawns; i++)
        {
            GameObject spawnedEnemy = GetPooledEnemy();
            if (spawnedEnemy != null)
            {
                spawnedEnemy.transform.position = new Vector3(Random.Range(-24f, 24f), 1, Random.Range(-24f, 24f));
                spawnedEnemy.transform.rotation = Quaternion.identity;
                spawnedEnemy.SetActive(true);
            }
        }
    }

    private GameObject GetPooledEnemy()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
