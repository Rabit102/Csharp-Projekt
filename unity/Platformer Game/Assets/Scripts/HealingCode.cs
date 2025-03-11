using System;
using UnityEngine;

public class Healing : MonoBehaviour
{

    [SerializeField] int addedLive = 1;
    [SerializeField] AudioClip liveSound;
    [SerializeField] bool maxitem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            AudioCode.instance.PlaySound(liveSound);
            player.healing(addedLive, maxitem);
            Destroy(gameObject);
        }
    }
}
