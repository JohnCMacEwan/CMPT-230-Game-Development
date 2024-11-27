using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float hp = 100f;    // Enemy's current health
    public float maxHp = 100f; // Enemy's maximum health

    public GameObject waveManagerObject;

    private WaveManager waveManager;

    // Start is called before the first frame update
    void Start()
    {
        waveManager = waveManagerObject.GetComponent<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This is called when a collision is detected
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with a bullet
        if (collision.gameObject.CompareTag("Player Bullet"))
        {
            TakeDamage(20f); // Deal 20 damage when hit by a bullet
        }
    }

    // Method to take damage and update the health bar
    public void TakeDamage(float damage)
    {
        hp -= damage;

        // Ensure hp doesn't go below zero
        if (hp < 0f)
        {
            hp = 0f;
        }

        // Destroy the enemy if health drops to zero
        if (hp <= 0f)
        {
            waveManager.aliveEnemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
