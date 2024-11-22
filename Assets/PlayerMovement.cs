using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{


    public float movementSpeed = 1f;
        
    public Rigidbody2D rb;
    public Camera cam;
    public AudioSource audioSrc;

    Vector2 movement;
    Vector2 mousePosition;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashCooldown = 1f;
    float nextDashTime = 0f;
    public bool isDashing;

    [Header("Shoot Settings")]
    public Transform Fire;
    public GameObject bullet;
    public AudioSource audioSrcShoot;

    public float bulletForce = 10f;

    // Update is called once per frame
    void Update()
    {

        if (isDashing) { return; }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextDashTime)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetButtonDown("Fire1"))
        {
            audioSrcShoot.Play();
            ShootBullet();
        }

    }

    private void FixedUpdate()
    {
        if (isDashing) { return; }

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

    private IEnumerator Dash()
    {
        isDashing = true;
        nextDashTime = Time.time + dashCooldown;
        rb.velocity = new Vector2(movement.x * dashSpeed, movement.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    void ShootBullet()
    {
        GameObject bulletObj = Instantiate(bullet, Fire.position, Fire.rotation);
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        rb.AddForce(Fire.up * bulletForce, ForceMode2D.Impulse);
    }




}
