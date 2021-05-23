using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightBullet : MonoBehaviour
{
    private float baseSpeed = 5f;
    private Rigidbody2D bullet;

    private GameObject lightEnemy;
    private Transform lightEnemyTrans;

    private void Awake()
    {
        bullet = GetComponent<Rigidbody2D>();
        lightEnemy = GameObject.FindGameObjectWithTag("enemy");

        lightEnemyTrans = lightEnemy.transform;

    }
    // Start is called before the first frame update
    void Start()
    {
        if (lightEnemy.GetComponent<SpriteRenderer>().flipX == false)
        {
            bullet.velocity = new Vector2(baseSpeed, bullet.velocity.y);
        }
        else
        {
            bullet.velocity = new Vector2(-baseSpeed, bullet.velocity.y);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
