using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class GameController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bombPickupPrefab;
    public GameObject healthPickupPrefab;
    public Transform player;
    public int maxEnemies = 1000;
    public int maxBombPickups = 2;
    public int maxHealthPickups = 4;
    public float maxTimeBetweenSpawns = 5f;
    public static GameController controller;

    private List<GameObject> pooledEnemies;
    private List<GameObject> pooledBombPickups;
    private List<GameObject> pooledHealthPickups;
    private bool isStarted;
    private float timer = 0f;

    public void Awake()
    {
#if !UNITY_EDITOR
        GarbageCollector.GCMode = GarbageCollector.Mode.Manual;
#endif
        controller = this;
        pooledEnemies = new List<GameObject>();
        pooledBombPickups = new List<GameObject>();
        pooledHealthPickups = new List<GameObject>();

        isStarted = false;
        Time.timeScale = 0;
    }

    public void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject temp = Instantiate(enemyPrefab);
            temp.GetComponent<EnemyBehaviour>().player = player;
            temp.SetActive(false);
            pooledEnemies.Add(temp);
        }
        for (int i = 0; i < maxBombPickups; i++)
        {
            GameObject temp = Instantiate(bombPickupPrefab);
            temp.SetActive(false);
            pooledBombPickups.Add(temp);
        }
        for (int i = 0; i < maxHealthPickups; i++)
        {
            GameObject temp = Instantiate(healthPickupPrefab);
            temp.SetActive(false);
            pooledHealthPickups.Add(temp);
        }
        StartCoroutine(SpawnTimer());
    }

    private void Update()
    {
        if (!isStarted && (Input.GetMouseButtonUp(0) || Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            isStarted = true;
            Time.timeScale = 1;
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
            SpawnPickup();
        }
        timer = 0f;
    }

    private GameObject GetPooledEnemy()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            GameObject obj = pooledEnemies[i];
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    private void SpawnPickup()
    {
        GameObject spawnedPickup = null;
        float type = Random.Range(0, 10);
        if (type == 0)
        {
            spawnedPickup = GetPooledBombPickup();
        }
        else if (type < 5)
        {
            spawnedPickup = GetPooledHealthPickup();
        }

        if (spawnedPickup != null)
        {
            spawnedPickup.transform.position = new Vector3(Random.Range(-24f, 24f), 1, Random.Range(-24f, 24f));
            spawnedPickup.transform.rotation = Quaternion.identity;
            spawnedPickup.SetActive(true);
        }
    }

    private GameObject GetPooledHealthPickup()
    {
        for (int i = 0; i < maxHealthPickups; i++)
        {
            GameObject obj = pooledHealthPickups[i];
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }
    
    private GameObject GetPooledBombPickup()
    {
        for (int i = 0; i < maxBombPickups; i++)
        {
            GameObject obj = pooledBombPickups[i];
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            if (timer < maxTimeBetweenSpawns)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            else
            {
                SpawnEnemies();
            }
        }
    }
}
