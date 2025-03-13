using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCode : MonoBehaviour
{
    public void OnstartClick()
    {
        SceneManager.LoadScene("LevelAuswahl");
    }

    public void OnexitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
