using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HighscoreCode : MonoBehaviour
{
    public CounterCode counterCode;
    public TextMeshProUGUI highscoreText;

    private void Start()
    {
        highscoreText.text = "Highscore: " + PlayerPrefs.GetFloat("Highscore").ToString("0.00");
    }

    public void Highscore()
    { 

        if (PlayerPrefs.GetFloat("Highscore") == 0)
        {
            PlayerPrefs.SetFloat("Highscore", counterCode.counter);
        }
        else if (counterCode.counter < PlayerPrefs.GetFloat("Highscore"))
        {
            PlayerPrefs.SetFloat("Highscore", counterCode.counter);
        }
    }
}
