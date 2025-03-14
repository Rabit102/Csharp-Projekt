using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCode : MonoBehaviour
{
    [SerializeField] AudioClip collectedSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            AudioCode.instance.PlaySound(collectedSound);
            SceneManager.LoadScene("StartMenu");
        }
    }
}
