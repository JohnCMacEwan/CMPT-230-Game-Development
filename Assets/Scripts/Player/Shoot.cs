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
    public AudioSource audioSrc;

    public float bulletForce = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            audioSrc.Play();
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject bulletObj = Instantiate(bullet, Fire.position, Fire.rotation);
        bulletObj.transform.SetParent(bulletsFolder.transform);
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();

        rb.AddForce(Fire.up * bulletForce, ForceMode2D.Impulse);
    }
}
