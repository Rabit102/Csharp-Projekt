using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public int shields;
    public GameObject energyshield;

    void Start()
    {
       currentHealth = maxHealth;
    }

    void Update()
    {
        if (shields == 0)
        {
            energyshield.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (shields > 0)
        {
            shields--;
            StartCoroutine(RegenerateShield(10f));
            return;
        }

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    IEnumerator RegenerateShield(float delay)
    {
        yield return new WaitForSeconds(delay);
        AddShield();
    }
    
    public void ResetTower()
    {
        currentHealth = maxHealth;
        shields = 0;
    }

    public void IncreaseMaxHealth(int amount) => currentHealth += amount;
    public void AddShield()
    {
        shields++;
        energyshield.SetActive(true);
    }
}
