using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rb;

    public float speed = 1.5f;
    public float rotateSpeed = 0.0025f;

    public float hp = 100;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!target)
        {
            getTarget();
        }
        else
        {
            RotateToTarget();
        }

    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    void getTarget ()
    {
        target = GameObject.FindGameObjectWithTag("Villain").transform;
    }

    void RotateToTarget()
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;

        Quaternion quiaternion = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, quiaternion, rotateSpeed);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Villain"))
        {
            Destroy(collision.gameObject);
            target = null;
        }

        else if (collision.gameObject.CompareTag("Bullet"))
        {
            hp -= 20f;
            if (hp < 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
