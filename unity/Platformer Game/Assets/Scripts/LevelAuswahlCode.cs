using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerCode : MonoBehaviour
{
    public bool unlocked = false;
    public Button button;
    public Image Image;

    public void Onlevel1Click()
    {
        SceneManager.LoadScene("Level1");
    }

    private void Update()
    {
        if (unlocked)
        {
            button.enabled = true;
            Destroy(Image);
        }
        else
        {
            button.enabled=false;
        }
    }

    public void Onlevel2Click()
    {
        SceneManager.LoadScene("Level2");
    }
}
