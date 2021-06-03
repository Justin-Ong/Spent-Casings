using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public float fireRate;
    public int maxCountdownCount;
    public int bombCount;

    private float timer = 0;
    private float countdownTimer = 0;
    private Rigidbody ourBody;
    private int currCountdownCount;
    private Vector3 prevPos;
    private Text CountdownText;
    private Text BombText;

    // Start is called before the first frame update
    void Start()
    {
        ourBody = GetComponent<Rigidbody>();
        currCountdownCount = maxCountdownCount;
        prevPos = transform.position;
        References.player = gameObject;
        CountdownText = References.countdown.GetComponent<Text>();
        CountdownText.text = currCountdownCount.ToString();
        BombText = References.bombCounter.GetComponent<Text>();
        BombText.text = bombCount.ToString();
    }

    void FixedUpdate()
    {
        Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        ourBody.velocity = directionVector * speed;

        if ((((transform.position - prevPos).magnitude) / Time.deltaTime) < 20)
        {
            countdownTimer += Time.deltaTime;
            if (countdownTimer >= 1)
            {
                currCountdownCount -= 1;
                countdownTimer = 0;
            }
        }
        else
        {
            countdownTimer = 0;
        }
        CountdownText.text = currCountdownCount.ToString();
        if (currCountdownCount <= 0)
        {
            Die();
        }

        prevPos = transform.position;
        BombText.text = bombCount.ToString();
    }

    private void LateUpdate()
    {
        Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        timer += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            if (timer > (1 / fireRate))
            {
                Shoot(directionVector);
                timer = 0;
            }
        }
        else
        {
            Vector3 facing = transform.position + directionVector;
            transform.LookAt(facing);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Bomb();
        }
    }


    private void Bomb()
    {
        if (bombCount > 0)
        {
            bombCount -= 1;
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        }
    }

    private void Shoot(Vector3 directionVector)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle + 90, 0));

        Bullet newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation).GetComponent(typeof(Bullet)) as Bullet;
        newBullet.inheritedMovement = ourBody.velocity;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<pickupBehaviour>() != null)
        {
            if (collision.gameObject.GetComponent<pickupBehaviour>().pickupType == "bomb")
            {
                bombCount += 1;
            }
            if (collision.gameObject.GetComponent<pickupBehaviour>().pickupType == "health")
            {
                currCountdownCount += 1;
            }
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
        renderer.gameObject.SetActive(false);
        References.countdown.GetComponent<UnityEngine.UI.Text>().text = "Game Over\nFinal score:\n" + References.score.ToString();
        References.isAlive = false;
    }
}
