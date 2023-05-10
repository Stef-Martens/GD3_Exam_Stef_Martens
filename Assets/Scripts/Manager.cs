using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Manager : MonoBehaviour
{
    public List<BaseAbility> Inventory;




    void Start()
    {
        Inventory = new List<BaseAbility>();
        Inventory.Add(FindObjectOfType<Shooting>());
    }

    void Update()
    {

        if (Gamepad.current.leftShoulder.IsPressed())
        {
            Time.timeScale = 0.05f;
        }
        else
            Time.timeScale = 1f;

        if (Gamepad.current.aButton.wasPressedThisFrame)
        {
            AddInventory(FindObjectOfType<Necromance>());
            AddInventory(FindObjectOfType<Shockwave>());
            AddInventory(FindObjectOfType<Freeze>());
            if (!Inventory.Contains(FindObjectOfType<Shooting>())) AddInventory(FindObjectOfType<Shooting>());
        }

    }

    public void AddInventory(BaseAbility i)
    {
        Inventory.Add(i);
    }
}
