using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp = 100f;
    public float maxHp = 100f;

    [SerializeField]
    private RectTransform healthBar;

    private Vector3 deathPos;

    public void Update()
    {
        if (healthBar.localScale == new Vector3(0, 1, 1)) gameObject.SetActive(false);
        // Set player's position manually, otherwise he will fly off to nowhere.
        if (hp <= 0) transform.position = deathPos;
        healthBar.localScale = Vector3.Slerp(healthBar.localScale, new Vector3(hp / maxHp, 1, 1), 10f * Time.deltaTime);
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

        if (hp <= 0f)
        {
            deathPos = transform.position;
            hp = 0f;
            foreach (MonoBehaviour component in gameObject.GetComponents<MonoBehaviour>())
            {
                if (component.Equals(this)) return;
                component.enabled = false;
            }
        }
    }
}