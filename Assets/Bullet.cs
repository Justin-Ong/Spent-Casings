using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 inheritedMovement;
    public float timeToLive;

    private float timer;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody ourBody = GetComponent<Rigidbody>();
        ourBody.velocity = transform.forward * speed;
        ourBody.velocity += inheritedMovement;
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToLive)
        {
            isActive = false;
        }
        if (transform.position.y < -25)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<WallBehaviour>() != null)
        {
            isActive = false;
        }
        else if (isActive == true && collision.gameObject.GetComponent<EnemyBehaviour>() != null)
        {
            EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
            if (enemy.currHealth > 0)
            {
                Destroy(gameObject);
            }
            enemy.TakeDamage();
        }
    }
}
