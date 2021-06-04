using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int maxHealth;
    public int currHealth;
    public GameObject player;
    public GameObject enemyPrefab;
    public LayerMask enemyLayer;

    private Rigidbody ourBody;
    private float speed;
    private bool isDead = false;
    private RaycastHit[] collisionDetectionArray;

    void Start()
    {
        ourBody = GetComponent<Rigidbody>();
        currHealth = maxHealth;
        speed = References.maxSpeed;
        collisionDetectionArray = new RaycastHit[1];
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            Vector3 vectorToPlayer = References.player.transform.position - transform.position;
            Vector3 distance = vectorToPlayer.normalized * speed * Time.deltaTime;
            Vector3 newPos = transform.position + distance;
            if (Physics.RaycastNonAlloc(transform.position, vectorToPlayer, collisionDetectionArray, distance.magnitude, enemyLayer) == 0) {
                ourBody.MovePosition(newPos);
            }
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
