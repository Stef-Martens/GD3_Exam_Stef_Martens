using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : BaseEnemy
{
    GameObject enemyToHeal;

    public float healRange = 3f;

    public float healRate = 2f;




    protected override void Update()
    {
        if (enemyToHeal)
        {
            if (Vector3.Distance(transform.position, enemyToHeal.transform.position) <= healRange)
            {
                enemyToHeal.GetComponent<BaseEnemy>().currentHealth += Time.deltaTime * healRate;

            }
            else
            {
                // Move towards the enemy to heal
                Vector3 direction = (enemyToHeal.transform.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
            }


            if (enemyToHeal.GetComponent<BaseEnemy>().currentHealth >= maxHealth)
                enemyToHeal = null;
        }
        else
        {
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
