using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [Header("ShieldSettings")]
    public float distance = 2f;
    public float moveSpeed = 5f;

    private Vector2 targetPosition;

    void Start()
    {
        targetPosition = new Vector2(distance, 0);
        transform.localPosition = targetPosition;
    }

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
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                bool isActive = GameManager.Instance.upgradePanel.activeSelf;
                GameManager.Instance.upgradePanel.SetActive(!isActive);
                Time.timeScale = isActive ? 1 : 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.RestartGame();
        }

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }
}
