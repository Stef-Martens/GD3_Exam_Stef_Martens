using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour
{

    public Button ContinueButton;

    public bool openShoot=false;


    void Start()
    {
        ContinueButton.Select();
    }


    public void CloseInstruction()
    {
        FindObjectOfType<Manager>().InstructionActive = false;
        if (openShoot) FindObjectOfType<Instructions>().OpenShootInstructions();
        gameObject.SetActive(false);
    }
}
