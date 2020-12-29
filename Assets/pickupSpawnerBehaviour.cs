using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupSpawnerBehaviour : MonoBehaviour
{
    public float spawnTimer;
    public GameObject healthPickupPrefab;
    public GameObject bombPickupPrefab;
    public float maxMisses;

    private float timer = 0;
    private float currMisses = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            if (currMisses > maxMisses)
            {
                float type = Random.Range(0, 5);
                if (type == 0)
                {
                    Instantiate(bombPickupPrefab, new Vector3(Random.Range(-24, 24), 1, Random.Range(-24, 24)), transform.rotation);
                }
                else
                {
                    Instantiate(healthPickupPrefab, new Vector3(Random.Range(-24, 24), 1, Random.Range(-24, 24)), transform.rotation);
                }
                currMisses = 0;
                Debug.Log("Miss spawn");
            }
            else
            {
                float type = Random.Range(0, 10);
                if (type == 0)
                {
                    Instantiate(bombPickupPrefab, new Vector3(Random.Range(-24, 24), 1, Random.Range(-24, 24)), transform.rotation);
                }
                else if (type <= 3)
                {
                    Instantiate(healthPickupPrefab, new Vector3(Random.Range(-24, 24), 1, Random.Range(-24, 24)), transform.rotation);
                }
                else
                {
                    currMisses += 1;
                }
                Debug.Log("Normal spawn");
            }
            timer = 0;
        }
    }
}
