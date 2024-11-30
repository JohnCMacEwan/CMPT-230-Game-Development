using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this to use UI components like Image

public class Enemy : MonoBehaviour
{
    public Transform target;  // The player that the enemy will follow
    private Rigidbody2D rb;   // Rigidbody2D for movement

    public float speed = 1.5f;  // Speed of the enemy movement
    public float rotateSpeed = 0.025f;  // Speed at which the enemy rotates towards the target

    public float distanceStop = 0f;

    [SerializeField]
    private List<AudioClip> footsteps;

    [SerializeField]
    private AudioSource source;

    private int clip = 0;
    private float lastFootstep = 0f;
    public float footstepSpeed = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        getTarget();
    }

    private void Update()
    {
        if (rb.velocity.magnitude >= 0.1f && Time.time > lastFootstep + footstepSpeed)
        {
            lastFootstep = Time.time;
            source.PlayOneShot(footsteps[clip]);
            clip++;

            if (clip >= footsteps.Count) clip = 0;
        }

        RotateToTarget();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer > distanceStop) {
            rb.velocity = transform.up * speed; // Move forward in the direction the enemy is facing
        } 
        else if (distanceStop - 0.05f < distanceToPlayer && distanceToPlayer < distanceStop + 0.05f)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (distanceToPlayer < distanceStop)
        {
            rb.velocity = -transform.up * (speed * 0.9f);
        }
    }

    // Method to find the target
    void getTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;  // Assuming "Player" is the tag of the player
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
}
