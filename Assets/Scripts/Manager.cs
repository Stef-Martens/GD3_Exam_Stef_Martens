using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Manager : MonoBehaviour
{
    public List<BaseAbility> Inventory;

    public float Health = 200f;

    public Text HealthText;

    Cauldron cauldron;

    public List<string> Ingredients;

    Map map;


    void Start()
    {
        Inventory = new List<BaseAbility>();
        Inventory.Add(FindObjectOfType<Shooting>());
        /*Inventory.Add(FindObjectOfType<Freeze>());
        Inventory.Add(FindObjectOfType<Shockwave>());
        Inventory.Add(FindObjectOfType<Necromance>());
        Inventory.Add(FindObjectOfType<FireComet>());*/
        cauldron = FindObjectOfType<Cauldron>();
        map = FindObjectOfType<Map>();
        
    }

    void Update()
    {
        HealthText.text = Health.ToString();

        if (Gamepad.current.leftShoulder.IsPressed() && !cauldron.MenuIsOpen && !map.IsOpen)
        {
            Time.timeScale = 0.05f;
        }
        else if (cauldron.MenuIsOpen)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }


        if (Gamepad.current.selectButton.wasPressedThisFrame && !cauldron.MenuIsOpen && !Gamepad.current.leftShoulder.IsPressed() && !map.IsOpen)
            map.IsOpen = true;
        else if (Gamepad.current.selectButton.wasPressedThisFrame && !cauldron.MenuIsOpen && !Gamepad.current.leftShoulder.IsPressed() && map.IsOpen)
            map.IsOpen = false;



    }

    public void AddInventory(BaseAbility i)
    {
        Inventory.Add(i);
    }
}
