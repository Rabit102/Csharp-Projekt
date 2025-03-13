using UnityEngine;

public class PlayerMenuCode : MonoBehaviour
{
    public float moveSpeed = 0.002f;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private int direction = 1;
    private float delaytoIdle;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grounded",true);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float directionX = mousePosition.x;

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

        rigidbody.linearVelocity = new Vector2(directionX / moveSpeed, rigidbody.linearVelocity.y);

        if (Mathf.Abs(directionX) > 0.01f)
        {
            delaytoIdle = 0.5f;
            animator.SetInteger("AnimState", 1);
        }
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
