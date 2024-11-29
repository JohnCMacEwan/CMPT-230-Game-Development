using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeHitbox;
    private SpriteRenderer meleeSprite;
    private CapsuleCollider2D capsuleCollider;

    private Health health;

    private List<Collider2D> hit = new List<Collider2D>();

    private float timeSinceLastAttack = 0;
    private float cooldown = 1f;

    private bool attacked = false;

    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip hitClip;
    [SerializeField]
    private AudioClip missClip;

    // Start is called before the first frame update
    void Start()
    {
        meleeSprite = meleeHitbox.GetComponent<SpriteRenderer>();
        capsuleCollider = meleeHitbox.GetComponent<CapsuleCollider2D>();
        health = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacked && Input.GetButtonDown("Fire2") && timeSinceLastAttack + cooldown < Time.time)
        {
            timeSinceLastAttack = Time.time;
            attacked = true;
            Attack();
        }
        else if (attacked && timeSinceLastAttack + 0.1f < Time.time)
        {
            meleeSprite.enabled = false;
            if (hit.Count > 0) source.PlayOneShot(hitClip);
            else source.PlayOneShot(missClip);
            hit = new List<Collider2D>();
            attacked = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attacked)
        {
            if (!hit.Contains(collision) && collision.gameObject.GetComponent<EnemyHealth>() != null)
            {
                hit.Add(collision);
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(30f);
                health.Heal(15f);
            }
        }
    }

    void Attack()
    {
        meleeSprite.enabled = true;
    }
}
