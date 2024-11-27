using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    private Transform target;

    public float attackCooldown = 2f;
    public float attackRange = 1f;

    private float timeSinceLastAttack;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastAttack = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToTarget = Vector2.Distance(target.position, transform.position);

        if (distanceToTarget <= attackRange && timeSinceLastAttack + attackCooldown < Time.time)
        {
            timeSinceLastAttack = Time.time;
            target.gameObject.GetComponent<Health>().TakeDamage(10f);
        }
    }
}
