using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float maxHealth = 100;
    public float currentHealth;

    protected Vector3 startPosition;
    protected bool isAttacking = false;
    protected Transform playerTransform;

    public bool Frozen = false;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        startPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (!Frozen)
        {
            if (!isAttacking)
            {
                MoveToStartPosition();
            }
            else
            {
                Attack();
            }
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }

    protected virtual void MoveToStartPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
    }

    protected virtual void Attack()
    {
        // implement attack logic for specific enemy types
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
