using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCode : MonoBehaviour
{
    [SerializeField] AudioClip collectedSound;
    public LevelAuswahlCode level;
    public HighscoreCode highscoreCode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            level.unlocked = true;
            AudioCode.instance.PlaySound(collectedSound);
            SceneManager.LoadScene("LevelAuswahl");
            highscoreCode.Highscore();
        }
    }
}
