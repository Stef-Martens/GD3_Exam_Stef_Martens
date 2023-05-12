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
        AddInventory(FindObjectOfType<Freeze>());
    }

    void Update()
    {




    }

    public void AddInventory(BaseAbility i)
    {
        Inventory.Add(i);
    }
}
