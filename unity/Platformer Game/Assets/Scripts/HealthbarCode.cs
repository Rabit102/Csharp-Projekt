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
            }
        }
        else
        {
            Debug.Log("Player not found");
        }
    }

    public void HealthUpdate(int live)
    {
        if (player != null)
        {
            for (int i = 0; i < player.Maxlive; i++)
            {
                if (i < live)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }
        }
    }
}
