using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys : MonoBehaviour
{
    public float speed = 75.0f;
    public float waitTime;
    public float startWaitTime = 1f;

    public bool canShoot = false;
    public float timeBtwShots;
    public float startBtwTimeShots = 5.0f;
    public float shootDistance = 100.0f;


    //public Transform[] moveSpots;
    private int point1;
    private int point2;
    public bool llegada = false;
    public bool isDead = false;

    public float deadTime;

    public Transform gamestop;
    public Transform gamestop1;
    public int hp = 10;

    public Rigidbody2D projectile;
    public GameObject LaunchPosition;

    public Transform player;



    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = 0f;
        waitTime = startWaitTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            isDead = true;
        }
        if (llegada == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            transform.position = Vector2.MoveTowards(transform.position, gamestop.position, speed * Time.deltaTime);
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (llegada == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            transform.position = Vector2.MoveTowards(transform.position, gamestop1.position, speed * Time.deltaTime);
            if (waitTime <= 0)
            {
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        // Disparo

        if (timeBtwShots <= 0 && llegada && player.transform.position.x > transform.position.x)
        {
            var bullet = Instantiate(projectile, LaunchPosition.transform.position, LaunchPosition.transform.rotation);
            Destroy(bullet.gameObject, 1.5f);
            timeBtwShots = startBtwTimeShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        if (timeBtwShots <= 0 && !llegada && player.transform.position.x < transform.position.x)
        {
            var bullet = Instantiate(projectile, LaunchPosition.transform.position, LaunchPosition.transform.rotation);
            Destroy(bullet.gameObject, 1.5f);
            timeBtwShots = startBtwTimeShots;
        }
        if (isDead)
        {
            deadTime += Time.deltaTime;
            if (deadTime >= 0.4f)
            {
                Destroy(gameObject);
            }
        }

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "movespot")
        {
            if (llegada)
            {
                llegada = false;
            }
            else
            {
                llegada = true;
            }
        }
        if (col.gameObject.tag == "Projectile")
        {
            hp -= 5;
            Destroy(col.gameObject);
            if (hp <= 0)
            {
                isDead = true;
            }
        }
    }

}
