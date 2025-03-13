using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("WaveControl")]
    public int totalWaves = 10;
    public int currentWave = 1;

    [Header("SpawnerLink")]
    public BulletSpawner bulletSpawner;

    [Header("UIElements")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject upgradePanel;
    public TextMeshProUGUI finalMessageText;

    [Header("Other")]
    public int score = 0;

    void Awake()
    {
        Instance = this;
    }
       void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void WaveCompleted()
    {
        if(currentWave <= totalWaves)
        {
            currentWave++;
            
            bulletSpawner.spawnInterval = Mathf.Max(0.5f, bulletSpawner.spawnInterval -0.1f);
            bulletSpawner.bulletsPerWave += 5;
            StartCoroutine(StartNextWave());
        }
        else
        {
            EndGame(true);
        }
        UpdateUI();
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(2f);
        bulletSpawner.Start();
    }

    public void GameOver()
    {
        EndGame(false);
    }

    void EndGame(bool victory)
    {
        Debug.Log("endgamestarted");
        bulletSpawner.StopAllCoroutines();
        if(victory)
        {
            finalMessageText.text = "Congratulations! Attack is over";
        }
        else
        {
            finalMessageText.text = "Tower destroyed!";
            upgradePanel.SetActive(true);
        }
    }

    void UpdateUI()
        {
            if(waveText != null)
            {
                waveText.text = "Wave: " + currentWave + " / " + totalWaves;
            }
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }



    void Update()
    {
        
    }
}
