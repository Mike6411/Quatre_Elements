using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster_Aim : MonoBehaviour
{
    public GameObject jugador;
    public GameObject rocket;
    public float rocketSpeed;

    public float shotDelay = 0.5f;
    public float elapsedTime = 0;

    void Start()
    {
        elapsedTime = 1f;
    }

    void Update()
    {
        //Rotation Call
        RotationWeapon(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //Shot delay stuff
        elapsedTime += Time.deltaTime;

        //Weapon Use
        if (Input.GetKeyDown(KeyCode.Mouse0) && elapsedTime >= shotDelay)
        {
            elapsedTime = 0;
            GameObject newRocket = Instantiate(rocket, transform.position + transform.right, transform.rotation);
            newRocket.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * rocketSpeed);
            Destroy(newRocket, 1.5f);
        }
    }

    public void RotationWeapon(Vector3 mousePos)
    {
        Vector2 dir = mousePos - transform.position;
        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, rotationZ);
    }

}
