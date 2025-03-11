using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    BoxCollider2D boxCollider;
    private SensorCode groundSensorL;
    private SensorCode groundSensorR;
    private SensorCode wallSensorRB;
    private SensorCode wallSensorRT;
    private SensorCode wallSensorLB;
    private SensorCode wallSensorLT;
    public HealthbarCode healthCode;

    [SerializeField] float rollingForce = 12.0f;// Rollkraft
    [SerializeField] float jumpForce = 7.5f; // Sprungkraft
    [SerializeField] float speed = 4.0f; // Geschwindigkeit der Bewegungen
    [SerializeField] public int live = 4; // Leben
    [SerializeField] public int Maxlive = 4; // Maximale Leben
    [SerializeField] public int coins = 0; // Münzen
    [SerializeField] private AudioClip swordSound; // Schwertklang
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip blockSound;
    [SerializeField] private AudioClip jumpSound;

    private bool rolling = false; // Rollen
    private bool grounded = false; // Bodenkontakt
    private bool crawling = false; // Kriechen

    private float sizeX = 0.75f; // Größe des Spielers
    private float sizeY = 1.2f; // Größe des Spielers
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
        boxCollider = GetComponent<BoxCollider2D>();
        groundSensorL = transform.Find("GroundSensorL").GetComponentInChildren<SensorCode>();
        groundSensorR = transform.Find("GroundSensorR").GetComponentInChildren<SensorCode>();
        wallSensorRB = transform.Find("WallSensorRB").GetComponentInChildren<SensorCode>();
        wallSensorRT = transform.Find("WallSensorRT").GetComponentInChildren<SensorCode>();
        wallSensorLB = transform.Find("WallSensorLB").GetComponentInChildren<SensorCode>();
        wallSensorLT = transform.Find("WallSensorLT").GetComponentInChildren<SensorCode>();
    }

    // Update is called once per frame
    void Update()
    {
        betweenAttack += Time.deltaTime;
        Crawl();
        if (rolling)
        {
            rollCurrentTime += Time.deltaTime;
            groundSensorL.Disable(0.2f);
            groundSensorR.Disable(0.2f);
        }

        if (rollCurrentTime > roltime)
        { 
            rolling = false;
            rollCurrentTime = 0;
            Crawl();
        }

        //check ob der Spieler auf dem Boden ist
        if (!grounded && groundSensorL.State() || groundSensorR.State())
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }

        //check ob der Spieler nicht auf dem Boden ist
        if (grounded && !groundSensorL.State() && !groundSensorR.State())
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }
        if (wallSensorRT.State() && wallSensorRB.State())
        {
            //Debug.Log("Wall");
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

        if (live <= 0) // Tod
        {
            //AudioCode.instance.PlaySound(deathSound);
            animator.SetBool("noBlood", false);
            animator.SetTrigger("Death");
        }


        else if (Input.GetMouseButtonDown(0) && betweenAttack > 0.25f && !rolling) // Angreifen
        {
            AudioCode.instance.PlaySound(swordSound);

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
            AudioCode.instance.PlaySound(blockSound);
            animator.SetTrigger("Block");
            animator.SetBool("IdleBlock", true);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("IdleBlock", false);
        }

        else if (Input.GetKeyDown("left shift") && !rolling)
        {
            rollCurrentTime = 0;
            rolling = true;
            boxCollider.size = new Vector2(sizeX, 0.8f);
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
        AudioCode.instance.PlaySound(jumpSound);
        animator.SetTrigger("Jump");
        grounded = false;
        animator.SetBool("Grounded", grounded);
        rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, jumpForce);
        groundSensorL.Disable(0.2f);
        groundSensorR.Disable(0.2f);
    }

    public void healing(int health,bool max)
    {
        if (Maxlive > live)
        {
            if (max)
            {
                live = Maxlive;
            }
            else
            {
                live += health;
            }
        }
    }

    internal void speeding(int addedSpeed)
    {
        speed += addedSpeed;
    }

    internal void moonMode(int addedJumpForce)
    {
        jumpForce += addedJumpForce;
    }

     public void hurt() // Schaden
     {
        AudioCode.instance.PlaySound(hurtSound);
        animator.SetTrigger("Hurt");
        live--;
        speed = 4.0f;
        healthCode.HealthUpdate(live);
     }


    public void Crawl()
    {
        if (wallSensorLT.State() || wallSensorRT.State())
        {
            boxCollider.autoTiling = true;
            crawling = true;
        }
        else
        {
            crawling = false;
            boxCollider.autoTiling = true;
        }
        animator.SetBool("Crawling", crawling);
    }
}
