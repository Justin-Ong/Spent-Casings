using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies()
    {
        int numSpawns = Random.Range(1, 3);
        for (int i = 0; i < numSpawns; i++)
        {
            GameObject temp = Instantiate(enemyPrefab, new Vector3(Random.Range(-24, 24), 1, Random.Range(-24, 24)), transform.rotation);
            temp.GetComponent<EnemyBehaviour>().player = playerPrefab;
        }
    }
}
