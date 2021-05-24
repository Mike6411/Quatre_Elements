using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement_Script : MonoBehaviour
{
    public float speed;
    public float jump;
    public int hp;
    private float speedx;
    public Rigidbody2D rb;
    private int counter = 0;
    public float maxX = 5;
    public AudioSource footstepsGrass;
    public SpriteRenderer sr;
    public GameObject fireball;

    public GameObject shield;

    public ParticleSystem shieldParticle; 
    public ParticleSystem jumpParticle; 
    private int walkParamID;
    private int jumpParamID;

   
    float moveVelocity;
    public bool grounded = true;
    public bool escut;

    public void Start()
    {
        hp = 10;
        //animator = GetComponent<Animator>();
        //walkParamID = Animator.StringToHash("Walk");
        //jumpParamID = Animator.StringToHash("Jump");
        escut = false;
    }

    void Update()
    {      
         bool isJumping = false;
        //Jumping 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpParticle.Play();
            isJumping = true;
            if (!grounded)
            {
                if (counter < 1)
                {
                    rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
                    counter++;
                }
            }
            else
            {
                rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
                counter = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!escut)
            {
                escut = true;
                shieldParticle.Play();
                shield.GetComponent<CircleCollider2D>().enabled = true;
                shield.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                escut = false;
                shieldParticle.Stop();
                shield.GetComponent<CircleCollider2D>().enabled = false;
                shield.GetComponent<ParticleSystem>().Stop();
            }
        }

        /*
        //Animator
        if (isJumping)
        {Debug.Log("salto");
            isJumping = animator.GetBool(jumpParamID);
            animator.SetBool(jumpParamID, true);
        }
        else {
            isJumping = animator.GetBool(jumpParamID);
            animator.SetBool(jumpParamID, false);
        }*/

        if (hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        bool isWalking = false;


        //Left Right Movement 
        if (Input.GetKey(KeyCode.A))
        {
            
            isWalking = true;
            if (rb.velocity.x > -maxX)
            {
                rb.AddForce((Vector2.left * speed), ForceMode2D.Force);
                sr.flipX = true;
            }

        }

        if (Input.GetKey(KeyCode.D))
        {
            
            isWalking = true;
            if (rb.velocity.x < maxX)
            {
                rb.AddForce((Vector2.right * speed) , ForceMode2D.Force);
                sr.flipX = false;
            }
            
        } 

        //Animator
        /*
        if (isWalking)
        {
            isWalking = animator.GetBool(walkParamID);
            animator.SetBool(walkParamID, true);
        }
        else
        {
            isWalking = animator.GetBool(walkParamID);
            animator.SetBool(walkParamID, false);
        }*/

    }
    //Check if Grounded 
    void OnCollisionStay2D()
    {
        jumpParticle.Stop();
        grounded = true;
        
    }
    void OnCollisionExit2D()
    {
        grounded = false;
    }

   
    public void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "grass") {
            footstepsGrass.Play();
         }

        if (collision.gameObject.tag == "Floppa")
        {
            SceneManager.LoadScene("EasterEgg");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.gameObject.tag == "lightBullet")
        {
            hp--;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "water")
        {
            hp = 10;
        }

        if (collision.gameObject.tag == "end")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
}
