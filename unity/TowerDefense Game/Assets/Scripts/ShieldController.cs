using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [Header("ShieldSettings")]
    public float distance = 2f;
    public float moveSpeed = 5f;

    private Vector2 targetPosition;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = new Vector2(distance, 0);
        transform.localPosition = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPosition = new Vector2(0, distance);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPosition = new Vector2(-distance, 0);
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPosition = new Vector2(0, -distance);
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPosition = new Vector2(distance, 0);
            transform.rotation = Quaternion.Euler(0, 0, 360);
        }

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }
}
