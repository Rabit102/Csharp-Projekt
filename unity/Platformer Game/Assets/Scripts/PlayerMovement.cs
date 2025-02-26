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
    bool grounded = true; // Bodenkontakt



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
        Debug.Log("außerhalb f "  +groundSensor.State());
        if(!grounded && groundSensor.State())
        {
            Debug.Log("innerhalb 1" + groundSensor.State());
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }

        if (grounded && !groundSensor.State())
        {
            Debug.Log("innerhalb 2 " + groundSensor.State());
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }


        float directionX = Input.GetAxis("Horizontal"); // Steuerungseingaben
        animator.SetFloat("AirSpeedY",rigidbody.linearVelocity.x);

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

        else if (Input.GetKeyDown("space") && grounded)
        {
            animator.SetTrigger("Jump");
            grounded = false;
            animator.SetBool("Grounded", grounded);
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
            groundSensor.Disable(0.2f);

        }

        //laufanimation
        else if (Mathf.Abs(directionX) > 0.01f)
        {
            delaytoIdle = 0.5f;
            animator.SetInteger("AnimState", 1);
            // Debug.Log(animator.GetInteger("AnimState"));
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
