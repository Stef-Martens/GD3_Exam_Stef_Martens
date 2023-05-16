using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Player")
            FindObjectOfType<MapIcon>().IsStuck = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            FindObjectOfType<MapIcon>().IsStuck = false;
    }

}
