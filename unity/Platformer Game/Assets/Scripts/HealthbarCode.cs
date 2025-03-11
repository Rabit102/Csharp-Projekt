using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarCode : MonoBehaviour
{
    public Image heartPrefap;
    public Sprite heart;

    public PlayerMovement player;

    private List<Image> hearts = new List<Image>();


    public void Start()
    {
        if (player != null)
        {
            Debug.Log("Player found");
            foreach (Image heart in hearts)
            {
                Destroy(heart.gameObject);
            }

            hearts.Clear();

            for (int i = 0; i < player.Maxlive; i++)
            {
                Image newHeart = Instantiate(heartPrefap, transform);
                newHeart.sprite = heart;
                hearts.Add(newHeart);
                Debug.Log("Heart " + i + " created");
            }
        }
        else
        {
            Debug.Log("Player not found");
        }
    }

    public void HealthUpdate(int live)
    {
        Debug.Log(hearts.Count);
        if (player != null)
        {
            for (int i = 0; i < player.Maxlive; i++)
            {
                Debug.Log("i:" + i + " live:" + live + "bool:");
                Debug.Log(i<live);
                if (i < live)
                {
                    hearts[i].enabled = true;
                    Debug.Log("Heart " + i + " enabled");
                }
                else
                {
                    hearts[i].enabled = false;
                    Debug.Log("Heart " + i + " disabled");
                }
                Debug.Log("Health updated");
            }
        }
    }
}
