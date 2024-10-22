using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this to use UI components like Image

public class Enemy : MonoBehaviour
{
    public Transform target;  // The player or "Villain" that the enemy will follow
    private Rigidbody2D rb;   // Rigidbody2D for movement

    public float speed = 1.5f;  // Speed of the enemy movement
    public float rotateSpeed = 0.0025f;  // Speed at which the enemy rotates towards the target

    public float hp = 100f;    // Enemy's current health
    public float maxHp = 100f; // Enemy's maximum health

    public Image healthBarImage;  // Reference to the HP bar's Image component (green bar)

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initialize health bar with the correct fill amount
        UpdateHealthBar();
    }

    private void Update()
    {
        if (!target)
        {
            getTarget(); // Assign a target if one isn't found
        }
        else
        {
            RotateToTarget(); // Rotate towards the target
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed; // Move forward in the direction the enemy is facing
    }

    // Method to find the target
    void getTarget()
    {
        target = GameObject.FindGameObjectWithTag("Villain").transform;  // Assuming "Villain" is the tag of the player
    }

    // Rotate towards the target
    void RotateToTarget()
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;

        // Smooth rotation
        Quaternion quaternion = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, quaternion, rotateSpeed);
    }

    // This is called when a collision is detected
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with a bullet
        if (collision.gameObject.CompareTag("Bullet"))
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

        // Update the health bar UI
        UpdateHealthBar();

        // Destroy the enemy if health drops to zero
        if (hp <= 0f)
        {
            Destroy(gameObject);
        }
    }

    // Update the health bar based on the current health
    void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = hp / maxHp;  // Adjust the green bar fill according to the enemy's current health
        }
    }
}
