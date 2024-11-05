using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 1f;

    public Rigidbody2D rb;
    public Camera cam;
    public AudioSource audioSrc;

    Vector2 movement;
    Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * movementSpeed * Time.deltaTime);

        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;

        if (movement.normalized.magnitude != 0)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
        }
        else { audioSrc.Stop(); }
    }
}
