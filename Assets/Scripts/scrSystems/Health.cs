using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField]
    public float health;

    public GameObject HitTextPrefab;

    // Start is called before the first frame update
    void Start()
    {      

        if (gameObject.CompareTag("Player"))
        {
            health = GetComponent<PlayerScript>().playerHP;
        }

        if (gameObject.GetComponent<Enemy>())
        {         
            health = GetComponent<Enemy>().enemyHP;
        }

        health = 10f;
    }
    public void SetEnemyHealth(float enemyHealth)
    {
        
    }

    public void SetHealth(float maxHealth, float health)
    {
        this.health = health;
    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;

        if (HitTextPrefab != null && health > 0)
        {
            ShowHitText();
        }

        if(health <= 0)
        {
            Die();
        }
    }

    void ShowHitText()
    {
        var go = Instantiate(HitTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = health.ToString();
    }

    private void Die()
    {
        //Debug.Log("I am Dead!");
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
