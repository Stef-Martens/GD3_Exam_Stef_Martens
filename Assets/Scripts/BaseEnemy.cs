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

    private float timer;

    public bool hit = false;

    public bool Takeover = false;

    private GameObject TakeoverEnemy;

    public GameObject SelectedObject;

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

            if (hit)
            {
                timer -= Time.deltaTime;

                // Check if the countdown has reached 0
                if (timer <= 0f)
                {
                    hit = false;
                }

                isAttacking = true;
            }

            if (Takeover)
            {
                if (!TakeoverEnemy)
                    AttackTakeover(FindClosestObject("Enemy"));
                else
                    AttackTakeover(TakeoverEnemy);
            }
            else
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
        if (Vector3.Distance(transform.position, startPosition) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            if (GetComponent<Animator>()) GetComponent<Animator>().Play("Walk");
        }

    }

    protected virtual void Attack()
    {
        // implement attack logic for specific enemy types
    }

    protected virtual void AttackTakeover(GameObject enemy)
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

    public void StartAttack()
    {
        hit = true;
        timer = 5;
    }

    public void DeselectTakover()
    {
        SelectedObject.SetActive(false);
    }

    public void SelectTakover()
    {
        SelectedObject.SetActive(true);
    }

    private GameObject FindClosestObject(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(gameObject.transform.position, obj.transform.position);

            if (distance < closestDistance && obj != gameObject)
            {
                closestObject = obj;
                closestDistance = distance;
            }
        }

        return closestObject;
    }

    public GameObject CheckObjectInCircle()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 4);

        foreach (Collider collider in colliders)
        {
            // Check if the collider has a component with the desired boolean value
            BaseEnemy enemy = collider.GetComponent<BaseEnemy>();
            if (enemy != null && enemy.Takeover)
            {
                return enemy.gameObject; // Found an object with the desired boolean value
            }
        }

        return null; // No object with the desired boolean value found
    }
}
