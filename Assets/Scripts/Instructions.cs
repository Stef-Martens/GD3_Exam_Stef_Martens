using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    public GameObject BaseInstructions;
    public GameObject ShootInstructions;
    public GameObject FreezeInstructions;
    public GameObject ShockwaveInstructions;
    public GameObject EnemyTakeoverInstructions;
    public GameObject NecromanceInstructions;
    public GameObject FireCometInstructions;
    public GameObject SuddenDeathInstructions;
    public GameObject InvisibilityInstructions;

    public void OpenBaseInstructions()
    {
        BaseInstructions.SetActive(true);
        FindObjectOfType<Manager>().InstructionActive = true;
    }
    public void OpenShootInstructions()
    {
        ShootInstructions.SetActive(true);
        FindObjectOfType<Manager>().InstructionActive = true;

    }
    public void OpenFreezeInstructions()
    {
        FreezeInstructions.SetActive(true);
        FindObjectOfType<Manager>().InstructionActive = true;

    }
    public void OpenShockwaveInstructions()
    {
        ShockwaveInstructions.SetActive(true);
        FindObjectOfType<Manager>().InstructionActive = true;

    }
    public void OpenEnemyTakeoverInstructions()
    {
        EnemyTakeoverInstructions.SetActive(true);
        FindObjectOfType<Manager>().InstructionActive = true;

    }
    public void OpenNecromanceInstructions()
    {
        NecromanceInstructions.SetActive(true);
        FindObjectOfType<Manager>().InstructionActive = true;

    }
    public void OpenFireCometInstructions()
    {
        FireCometInstructions.SetActive(true);
        FindObjectOfType<Manager>().InstructionActive = true;

    }
    public void OpenSuddenDeathInstructions()
    {
        SuddenDeathInstructions.SetActive(true);
        FindObjectOfType<Manager>().InstructionActive = true;

    }
    public void OpenInvisibilityInstructions()
    {
        InvisibilityInstructions.SetActive(true);
        FindObjectOfType<Manager>().InstructionActive = true;

    }

}
