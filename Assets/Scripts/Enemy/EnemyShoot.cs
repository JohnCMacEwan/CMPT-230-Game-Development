using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject enemyBulletsFolder;

    public Transform Fire;
    public GameObject bullet;

    public float bulletForce = 10f;

    public float shotTime = 1f;

    private float lastShot;
    [SerializeField]
    private AudioSource shotSound;

    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastShot + shotTime < Time.time)
        {
            lastShot = Time.time;
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject bulletObj = Instantiate(bullet, Fire.position, Fire.rotation);
        bulletObj.transform.SetParent(enemyBulletsFolder.transform);
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        rb.AddForce(Fire.up * bulletForce, ForceMode2D.Impulse);

        shotSound.PlayOneShot(shotSound.clip);
    }
}
