using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuCode : MonoBehaviour
{
    [SerializeField] GameObject pausemenu;
    public void PauseGame()
    {
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnResumeClick()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnExitClick()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }

    public void OnMenuClick()
    {
        SceneManager.LoadScene("LevelAuswahl");
        Time.timeScale = 1f;
    }
}
