using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp = 100f;
    public float maxHp = 100f;

    [SerializeField]
    private RectTransform healthBar;

    [SerializeField]
    private GameObject gameOverPanel;

    private Vector3 deathPos;




    public void Update()
{
    if (healthBar.localScale == new Vector3(0, 1, 1)) gameObject.SetActive(false);
    
    // Check if health is 0 and show game over screen
    if (hp <= 0)
    { // Display the game over panel
    ShowGameOver();
        transform.position = deathPos; // Optional: Set position to death position
    }

    // Update health bar display
    healthBar.localScale = Vector3.Slerp(healthBar.localScale, new Vector3(hp / maxHp, 1, 1), 10f * Time.deltaTime);
}

    private void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Show the game over panel
        }

        // Disable player movement or actions here if necessary
        foreach (MonoBehaviour component in gameObject.GetComponents<MonoBehaviour>())
        {
            if (component.Equals(this)) continue;
            component.enabled = false; // Disable all other components (e.g., movement)
        }
    }

    private void HideGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Hide the game over panel
        }

        // Optionally, re-enable player movement or actions if you want them to be able to retry
        foreach (MonoBehaviour component in gameObject.GetComponents<MonoBehaviour>())
        {
            if (component.Equals(this)) continue;
            component.enabled = true; // Re-enable components
        }
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

    public void Heal(float regen)
    {
        hp += regen;

        if (hp > maxHp) hp = maxHp;
    }

        // Retry the game (reload current scene)
    public void RetryGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit the game
    public void QuitGame()
    {
        // Exit the game
        Application.Quit();
    }
}