using Unity.VisualScripting;
using UnityEngine;

public class SpeedItemCode : MonoBehaviour
{
    [SerializeField] int addedSpeed = 3;
    [SerializeField] AudioClip collectedSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            AudioCode.instance.PlaySound(collectedSound);
            player.speeding(addedSpeed);
            Destroy(gameObject);
        }
    }
}
