using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : BaseEnemy
{

    public float attackRange = 2f;

    float lastAttackTime;


    protected override void Attack()
    {


        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            // Check if 2 seconds have elapsed since the last attack
            if (Time.time - lastAttackTime >= 2f)
            {
                Debug.Log("hit");

                // Update the last attack time
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // Move towards the player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }


    }
}
