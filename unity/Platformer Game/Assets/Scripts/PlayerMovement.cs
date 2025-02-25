using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private GroundSensor groundSensor;


    float jumpForce = 7.5f; // Sprungkraft
    float speed = 4.0f; // Geschwindigkeit der Bewegungen
    float delaytoIdle = 0.0f; // Verzögerung der Bewegungen
    int direction = 1; // Richtung der Bewegungen
    bool isGrounded = false; // Bodenkontakt



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        groundSensor = GetComponentInChildren<GroundSensor>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!isGrounded && groundSensor.State())
        {
            isGrounded = true;
            animator.SetBool("Grounded", isGrounded);
        }

        if (isGrounded && !groundSensor.State())
        {
            isGrounded = false;
            animator.SetBool("Grounded", isGrounded);
        }


        float directionX = Input.GetAxis("Horizontal"); // Steuerungseingaben

        rigidbody.linearVelocity = new Vector2(directionX * speed, rigidbody.linearVelocity.y); //Bewegung

        if (directionX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            direction = 1;
        }
        else if (directionX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            direction = -1;
        }

        if (Input.GetKeyDown("e"))
        {
            animator.SetBool("noBlood", true);
            animator.SetTrigger("Death");
        }

        else if (Input.GetKeyDown("space") && isGrounded)
        {
            animator.SetTrigger("Jump");
            isGrounded = false;
            animator.SetBool("Grounded", isGrounded);
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
            groundSensor.Disable(0.2f);

        }


        //laufanimation
        else if (Mathf.Abs(directionX) > 0.01f)
        {
            delaytoIdle = 0.5f;
            animator.SetBool("isRunning", true);
            Debug.Log(animator.GetInteger("AnimState"));
        }
        //steht still animation
        else
        {
            delaytoIdle -= Time.deltaTime;
            if (delaytoIdle < 0)
            {
                animator.SetInteger("AnimState", 0);
            }
        }
    }

}
