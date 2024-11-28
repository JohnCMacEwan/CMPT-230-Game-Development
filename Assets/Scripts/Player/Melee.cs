using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeHitbox;
    private SpriteRenderer meleeSprite;
    private CapsuleCollider2D capsuleCollider;

    private List<Collider2D> hit = new List<Collider2D>();

    private float timeSinceLastAttack = 0;
    private float cooldown = 1f;

    private bool attacked = false;

    // Start is called before the first frame update
    void Start()
    {
        meleeSprite = meleeHitbox.GetComponent<SpriteRenderer>();
        capsuleCollider = meleeHitbox.GetComponent<CapsuleCollider2D>();
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
            hit = new List<Collider2D>();
            attacked = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attacked)
        {
            if (!hit.Contains(collision))
            {
                hit.Add(collision);
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(30f);
            }
        }
    }

    void Attack()
    {
        meleeSprite.enabled = true;
    }
}
