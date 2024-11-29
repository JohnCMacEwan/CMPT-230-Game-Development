using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletsFolder;

    public Transform Fire;
    public GameObject bullet;

    public float bulletForce = 10f;

    private Health health;

    [SerializeField]
    private AudioSource shotSource;

    [SerializeField]
    private AudioClip clip;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject bulletObj = Instantiate(bullet, Fire.position, Fire.rotation);
        bulletObj.transform.SetParent(bulletsFolder.transform);
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();

        rb.AddForce(Fire.up * bulletForce, ForceMode2D.Impulse);
        health.TakeDamage(2f);
        shotSource.PlayOneShot(clip);
    }
}
