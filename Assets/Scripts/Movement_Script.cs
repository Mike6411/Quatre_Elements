using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement_Script : MonoBehaviour
{
    public float speed;
    public float jump;
    private float speedx;
    public Rigidbody2D rb;
    private int counter = 0;
    public float maxX = 5;
    public AudioSource footstepsGrass;
    public SpriteRenderer sr;
    public GameObject fireball;

    
    
    private int walkParamID;
    private int jumpParamID;

   
    float moveVelocity;
    public bool grounded = true;

    public void Start()
    {
        footstepsGrass = GetComponent<AudioSource>();
        //animator = GetComponent<Animator>();
        //walkParamID = Animator.StringToHash("Walk");
        //jumpParamID = Animator.StringToHash("Jump");
       
    }

    void Update()
    {
      
         bool isJumping = false;
        //Jumping 
        if (Input.GetKeyDown(KeyCode.Space))
        {
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
        if (collision.gameObject.tag == "car")
        {
            //Game_Manager.Instance.LoadNextScene();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("NEXT");
        }

        if (collision.gameObject.tag == "Finale") 
        {
            SceneManager.LoadScene("Final");
        }

        
    }
    
}
