using UnityEngine;

public class CameraCode : MonoBehaviour
{
    [SerializeField] private float speed;
    private float positionX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;
    [SerializeField] private float ahead;
    [SerializeField] private float camearaSpeed;
    private float lookahead;


    private void Update()
    {
        transform.position = new Vector3(player.position.x + lookahead, player.position.y + 4, transform.position.z);
        lookahead = Mathf.Lerp(lookahead,(ahead * player.localScale.x), Time.deltaTime* camearaSpeed);
    }

    public void Moving(Transform transform)
    {
        positionX = transform.position.x;
    }
}
