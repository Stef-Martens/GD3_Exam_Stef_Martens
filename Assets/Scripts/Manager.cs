using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Manager : MonoBehaviour
{
    public List<int> AvailablePowers;
    public List<int> Inventory;


    void Start()
    {
        AvailablePowers = new List<int>();
        Inventory = new List<int>();
        AvailablePowers.Add(1);
    }

    void Update()
    {
        if (Gamepad.current.leftShoulder.IsPressed())
        {
            Time.timeScale = 0.05f;
        }

        else
            Time.timeScale = 1f;
    }

    public void AddInventory(int i)
    {
        Inventory.Add(i);
    }
}
