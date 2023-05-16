using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform playerPos;
    public Transform offscreenPos;
    public float speed;

    public bool IsOpen = false;

    void Update()
    {
        if (!IsOpen)
            transform.position = offscreenPos.position;
        else
            transform.position = playerPos.position;

    }
}
