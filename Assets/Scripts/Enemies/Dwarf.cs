using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dwarf : BaseEnemy
{

    public float attackRange = 2f;

    float lastAttackTime;

    public float DamageDone;

    Animator animator;

    void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        lastAttackTime = -2;
    }


    protected override void Attack()
    {
        GameObject foundObject = CheckObjectInCircle();

        if (foundObject)
        {
            AttackTakeover(foundObject);
        }
        else
        {
            transform.LookAt(playerTransform);

            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
            {
                // Check if 2 seconds have elapsed since the last attack
                if (Time.time - lastAttackTime >= 2f)
                {
                    FindObjectOfType<Manager>().Health -= DamageDone;

                    // Update the last attack time
                    lastAttackTime = Time.time;
                    GetComponent<AudioSource>().Play();
                    animator.Play("Lumbering");
                }
            }
            else
            {
                animator.Play("Walk");
                // Move towards the player
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }
        }



    }
    protected override void AttackTakeover(GameObject enemy)
    {
        //base.AttackTakeover(enemy);

        transform.LookAt(enemy.transform);
        if (Vector3.Distance(transform.position, enemy.transform.position) <= attackRange)
        {
            // Check if 2 seconds have elapsed since the last attack
            if (Time.time - lastAttackTime >= 2f)
            {
                enemy.GetComponent<BaseEnemy>().TakeDamage((int)DamageDone);

                // Update the last attack time
                lastAttackTime = Time.time;
                GetComponent<AudioSource>().Play();
                animator.Play("Lumbering");
            }
        }
        else
        {
            animator.Play("Walk");
            // Move towards the player
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }




}
