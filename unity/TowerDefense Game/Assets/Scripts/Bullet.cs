using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("BulletSettings")]
    public float speed = 5f;
    public Vector2 direction = Vector2.zero;

    [Header("Decoy?")]
    public bool isDecoy = false;
    public float decoyTriggerDistance = 5f;

    private Transform towerTransform;

    void Start()
    {
        GameObject tower = GameObject.FindWithTag("Tower");
        if(tower.transform != null) {towerTransform = tower.transform;}
        UpdateRotation();
    }

    void Update()
    {
        if(isDecoy && towerTransform != null)
        {
            float distance = Vector2.Distance(transform.position, towerTransform.position);
            
            if(distance < decoyTriggerDistance)
            {
                direction = -direction;
                isDecoy = false;
                UpdateRotation();
            }
        }
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield"))
        {
            Destroy(gameObject);
            GameManager.Instance.AddScore(10);
        }
        else if (collision.CompareTag("Tower"))
        {
            Destroy(gameObject);
            GameManager.Instance.tower.TakeDamage(1);
        }
    }
    void UpdateRotation()
    {
        if (direction != Vector2.zero)
        {
            if(direction == Vector2.up) transform.rotation = Quaternion.Euler(0,0,90);
        else if(direction == Vector2.down) transform.rotation = Quaternion.Euler(0,0,270);
        else if(direction == Vector2.left) transform.rotation = Quaternion.Euler(0,0,180);
        else if(direction == Vector2.right) transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
