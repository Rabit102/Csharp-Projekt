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
        if(Input.GetKeyDown(KeyCode.W))
        {
            targetPosition = new Vector2(0, distance);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            targetPosition = new Vector2(-distance, 0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            targetPosition = new Vector2(0, -distance);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            targetPosition = new Vector2(distance, 0);
        }

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }
}
