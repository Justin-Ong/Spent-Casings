using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    public GameObject player;
    public GameObject enemyPrefab;

    private Rigidbody ourBody;
    private float speed;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        ourBody = GetComponent<Rigidbody>();
        currHealth = maxHealth;
        speed = References.maxSpeed;
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            Vector3 vectorToPlayer = References.player.transform.position - transform.position;
            ourBody.velocity = vectorToPlayer.normalized * speed;
            //ourBody.AddForce(vectorToPlayer.normalized * speed, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<explosionBehaviour>() != null)
        {
            if (!isDead)
            {
                Die(true);
            }
            Rigidbody ownBody = GetComponent<Rigidbody>();
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;
            ownBody.AddForce(80 * dir, ForceMode.Impulse);
        }
    }

    public void TakeDamage()
    {
        if (currHealth > 0)
        {
            currHealth -= 1;
        }
        if (!isDead && currHealth == 0)
        {
            Die(true);
        }
    }

    public void Die(bool countScore)
    {
        GameController.controller.SpawnEnemies();
        /*
        int numSpawns = Random.Range(1, 3);
        for (int i = 0; i < numSpawns; i++) {
            GameObject temp = Instantiate(gameObject, new Vector3(Random.Range(-24, 24), 1, Random.Range(-24, 24)), transform.rotation);
            temp.GetComponent<EnemyBehaviour>().enemyPrefab = enemyPrefab;
            temp.GetComponent<EnemyBehaviour>().currHealth = maxHealth;
            temp.GetComponent<EnemyBehaviour>().speed = References.maxSpeed;
        }
        */
        if (countScore)
        {
            References.score += 1;
        }
        isDead = true;
    }
}
