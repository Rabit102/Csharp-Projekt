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

    [Header("References")]
    public BulletSpawner bulletSpawner;
    public Tower tower;

    [Header("UIElements")]
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject upgradePanel;
    public TextMeshProUGUI finalMessageText;

    [Header("Other")]
    public int score = 0;
    public float scoreMultiplier = 1f;

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
        score += Mathf.RoundToInt(amount * scoreMultiplier);
        UpdateUI();
    }

    public void WaveCompleted()
    {
        if(currentWave < totalWaves)
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
            finalMessageText.text = "Tower destroyed!\nTry Again";
        }
    }

    public void RestartGame()
{
    currentWave = 1;
    score = 0;
    scoreMultiplier = 1f;

    foreach (var upgrade in UpgradeManager.Instance.upgrades)
    {
        upgrade.currentLevel = 0;
    }

    tower.ResetTower();
    bulletSpawner.ResetSpawner();

    Time.timeScale = 1;
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
}
