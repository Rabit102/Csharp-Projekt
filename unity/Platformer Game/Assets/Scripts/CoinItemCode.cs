using TMPro;
using UnityEngine;

public class CoinItemCode : MonoBehaviour
{
    [SerializeField] AudioClip collectedSound;

    public TextMeshProUGUI coinstext;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            AudioCode.instance.PlaySound(collectedSound);
            player.coins++;
            Destroy(gameObject);
            Debug.Log(player.coins);
            coinstext.text = player.coins.ToString("000");
        }
    }
}
