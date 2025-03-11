using UnityEngine;

public class TrapCode : MonoBehaviour
{
    public AudioClip trapSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            AudioCode.instance.PlaySound(trapSound);
            player.hurt();
        }
    }
}
