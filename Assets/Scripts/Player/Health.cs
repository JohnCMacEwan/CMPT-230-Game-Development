using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp = 100f;
    public float maxHp = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // This is called when a collision is detected
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Bullet"))
        {
            TakeDamage(20f);
        }
    }

    // Method to take damage and update the health bar
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp < 0f)
        {
            hp = 0f;
        }

        if (hp <= 0f)
        {
            Destroy(gameObject);
        }
    }
}