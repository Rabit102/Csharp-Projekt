using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class BulletSpawner : MonoBehaviour
{

    public static BulletSpawner Instance;

    [Header("SpawnSettings")]
    public GameObject bulletPrefab;
    public float spawnInterval = 1.5f;
    public int bulletsPerWave = 20;
    private int bulletsSpawned = 0;
    public float bulletSpeedMultiplier = 1f;
    public float baseBulletSpeed = 5f;

    void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        bulletsSpawned = 0;
        while (bulletsSpawned < bulletsPerWave)
        {
            SpawnBullet();
            bulletsSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
        GameManager.Instance.WaveCompleted();
    }

    public void ResetSpawner()
    {
        bulletsSpawned = 0;
        bulletSpeedMultiplier = 1f;
        StopAllCoroutines();
    }

    void SpawnBullet()
    {
        int side = Random.Range(0,4);
        Vector2 spawnPosition = Vector2.zero;
        Vector2 moveDirection = Vector2.zero;
        
        switch(side)
        {
            case 0:
            spawnPosition = new Vector2(0, 6);
            moveDirection = Vector2.down;
            break;
            
            case 1:
            spawnPosition = new Vector2(11, 0);
            moveDirection = Vector2.left;
            break;

            case 2:
            spawnPosition = new Vector2(0, -6);
            moveDirection = Vector2.up;
            break;

            case 3:
            spawnPosition = new Vector2(-11, 0);
            moveDirection = Vector2.right;
            break;
        }

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.direction = moveDirection;

        if(GameManager.Instance.currentWave > 3)
        {
            if(Random.value <0.2f)
            {
                bulletScript.isDecoy = true;
            }
        }
        
        bulletScript.speed += (baseBulletSpeed + GameManager.Instance.currentWave * 0.5f) * bulletSpeedMultiplier;
    }
}
