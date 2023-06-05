using System.Collections;
using System.Collections.Generic;
using FischlWorks_FogWar;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector3 Destination;
    private int radius = 8;
    private int force = 100;
    public int IndexRevealer;
    public GameObject FireParticle;

    void Update()
    {
        if (Destination != null)
        {
            Vector3 direction = Destination - transform.position;
            float distance = direction.magnitude;
            direction.Normalize();

            float moveDistance = Mathf.Min(12 * Time.deltaTime, distance);
            transform.Translate(direction * moveDistance, Space.World);
        }
        if (transform.position == Destination)
            OnCollisionEnter(null);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in hitColliders)
        {

            if (col.GetComponent<BaseEnemy>())
            {
                Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Vector3 direction = rb.transform.position - transform.position - new Vector3(0, 5, 0);
                    float distance = direction.magnitude;

                    float falloff = 1 - Mathf.Clamp01(distance / radius);
                    Vector3 forceVector = direction.normalized * force * falloff;

                    rb.AddForce(forceVector, ForceMode.Impulse);
                }
                col.GetComponent<BaseEnemy>().TakeDamage(80);
            }
        }

        FindObjectOfType<csFogWar>().RemoveFogRevealer(IndexRevealer);
        FindObjectOfType<SoundManager>().PlayFireCometEndSound();
        Instantiate(FireParticle, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}
