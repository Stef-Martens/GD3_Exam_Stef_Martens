using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class MapIcon : MonoBehaviour
{
    public bool IsStuck = false;
    public void Move(Vector3 direction)
    {
        if (!IsStuck)
        {
            //direction.y = 0;
            transform.position += direction;
        }

    }
}
