using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeleteBullet", 2f);
    }

    void DeleteBullet()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<BaseEnemy>())
        {
            collision.gameObject.GetComponent<BaseEnemy>().TakeDamage(20);
            collision.gameObject.GetComponent<BaseEnemy>().StartAttack();
        }


        DeleteBullet();
    }
}
