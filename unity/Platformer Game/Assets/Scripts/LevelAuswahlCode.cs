using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAuswahlCode : MonoBehaviour
{
    public void Onlevel1Click()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void Onlevel2Click()
    {
        SceneManager.LoadScene("LvL2");
    }
}
