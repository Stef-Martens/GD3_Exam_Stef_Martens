using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : BaseEnemy
{
    GameObject enemyToHeal;

    public float healRange = 5f;

    public float healDuration = 5f;
    public float healRate = 2f;

    private float healTimer = 0f;



    protected override void Update()
    {
        if (enemyToHeal)
        {
            if (Vector3.Distance(transform.position, enemyToHeal.transform.position) <= healRange)
            {
                healTimer += Time.deltaTime;

                // Gradually increase the player's health over the heal duration
                if (healTimer < healDuration)
                {
                    enemyToHeal.GetComponent<BaseEnemy>().currentHealth += Time.deltaTime * healRate;
                }
            }
            else
            {
                healTimer = 0f;
                // Move towards the enemy to heal
                Vector3 direction = (enemyToHeal.transform.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }


            if (enemyToHeal.GetComponent<BaseEnemy>().currentHealth >= maxHealth)
                enemyToHeal = null;
        }
        else
        {
            healTimer = 0f;
            MoveToStartPosition();

            Collider[] colliders = Physics.OverlapSphere(playerTransform.position, 6f);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.GetComponent<BaseEnemy>())
                {
                    if (collider.gameObject.GetComponent<BaseEnemy>().currentHealth < maxHealth)
                    {
                        enemyToHeal = collider.gameObject;
                        break;
                    }
                }
            }
        }
    }
}
