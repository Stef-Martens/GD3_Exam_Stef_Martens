using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : BaseEnemy
{
    public float attackRange = 2f;

    public float SpawnInterval = 10f;

    float lastAttackTime;

    public GameObject SkeletonsPrefab;


    protected override void Attack()
    {
        transform.LookAt(playerTransform);
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            // Check if 2 seconds have elapsed since the last attack
            if (Time.time - lastAttackTime >= SpawnInterval)
            {
                GetComponent<AudioSource>().Play();
                SpawnSkeletons();
                // Update the last attack time
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // Move towards the player
            GetComponent<Animator>().Play("Walk");
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void SpawnSkeletons()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle * 3;
            Vector3 spawnPos = new Vector3(randomPos.x, 0.5f, randomPos.y) + transform.position;
            GameObject skelet = Instantiate(SkeletonsPrefab, spawnPos, Quaternion.identity);
            //skelet.transform.parent = gameObject.transform;
        }
    }
}
