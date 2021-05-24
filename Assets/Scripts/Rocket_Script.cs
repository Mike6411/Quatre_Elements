using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Script : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       /* if (collision.gameObject.tag != "Untagged")
        {
            Destroy(gameObject);
        }*/
    }
}
