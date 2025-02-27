using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SensorCode groundSensor;
    private SensorCode wallSensorRB;
    private SensorCode wallSensorRT;
    private SensorCode wallSensorLB;
    private SensorCode wallSensorLT;

    [SerializeField] float rollingForce = 6.0f;// Rollkraft
    [SerializeField] float jumpForce = 7.5f; // Sprungkraft
    [SerializeField] float speed = 4.0f; // Geschwindigkeit der Bewegungen

    private bool rolling = false; // Rollen
    private bool grounded = false; // Bodenkontakt

    private float delaytoIdle = 0.0f; // Verzögerung der Bewegungen
    private float roltime = 8.0f / 14.0f; // Rollzeit
    private float rollCurrentTime; // Aktuelle Rollzeit
    private float betweenAttack = 0.0f; // Zeit zwischen den Angriffen

    private int currentAttack = 0; // Aktueller Angriff
    private int direction = 1; // Richtung der Bewegungen
    private bool extrajump;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        groundSensor = GetComponentInChildren<SensorCode>();
        wallSensorRB = GetComponentInChildren<SensorCode>();
        wallSensorRT = GetComponentInChildren<SensorCode>();
        wallSensorLB = GetComponentInChildren<SensorCode>();
        wallSensorLT = GetComponentInChildren<SensorCode>();
    }

    // Update is called once per frame
    void Update()
    {
        betweenAttack += Time.deltaTime;

        if (rolling)
        {
            rollCurrentTime += Time.deltaTime;
        }

        if(rollCurrentTime > roltime)
        {
            rolling = false;
        }

        if (!grounded && groundSensor.State())
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }

        if (grounded && !groundSensor.State())
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }
        if (!wallSensorRT.State() && !wallSensorRB.State())
        {
            Debug.Log("Wall");
        }
        
        float directionX = Input.GetAxis("Horizontal"); // Steuerungseingaben

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

        if (!rolling)
        {
            rigidbody.linearVelocity = new Vector2(directionX * speed, rigidbody.linearVelocity.y);
        }

        //Bewegung
        rigidbody.linearVelocity = new Vector2(directionX * speed, rigidbody.linearVelocity.y); 

        animator.SetFloat("AirSpeedY", rigidbody.linearVelocity.y);

        if (Input.GetKeyDown("e") && !rolling) // Tod
        {
            animator.SetBool("noBlood", false);
            animator.SetTrigger("Death");
        }

        else if (Input.GetKeyDown("q") && !rolling) // Schaden
        {
            animator.SetTrigger("Hurt");
        }

        else if (Input.GetMouseButtonDown(0) && betweenAttack > 0.25f && !rolling) // Angreifen
        {
            currentAttack++;

            if (currentAttack > 3)
            {
                currentAttack = 1;
            }

            if(betweenAttack > 1.0f)
            {
                currentAttack = 1;
            }
            
            animator.SetTrigger("Attack" + currentAttack);

            betweenAttack = 0.0f;
        }

        else if (Input.GetMouseButtonDown(1) && !rolling) // Blocken
        {
            animator.SetTrigger("Block");
            animator.SetBool("IdleBlock", true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("IdleBlock", false);
        }

        else if (Input.GetKeyDown("left shift") && !rolling)
        {
            rolling = true;
            animator.SetTrigger("Roll");
            rigidbody.linearVelocity = new Vector2(direction * rollingForce, rigidbody.linearVelocity.y);
        }

        else if (Input.GetKeyDown("space") && grounded)
        {
            jump();
            extrajump = true;
        }

        else if(Input.GetKeyDown("space") && !grounded && extrajump)
        {
            jump();
            extrajump = false;
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

    private void jump()
    {
        animator.SetTrigger("Jump");
        grounded = false;
        animator.SetBool("Grounded", grounded);
        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
        groundSensor.Disable(0.2f);
    }
}
