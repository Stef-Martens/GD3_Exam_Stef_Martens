using System.Collections;
using System.Collections.Generic;
using FischlWorks_FogWar;
using UnityEngine;

public class FogRevealerAdd : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<csFogWar>().AddFogRevealer(new csFogWar.FogRevealer(transform, 5, true));
        }
    }
}
