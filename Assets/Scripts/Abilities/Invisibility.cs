using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override float CooldownTime
    {
        get { return 5; }
    }


    public override void Update()
    {
        if (!FindObjectOfType<Manager>().PauseMenu.activeSelf)
        {
            if (FindObjectOfType<Manager>().Inventory.Contains(this))
            {
                ChangeUI(CircleColor, Image, AbilityName, Description);
            }

            if (FindObjectOfType<MenuScript>().currentAbility == this)
            {

            }
        }
    }


    public override void Start()
    {

    }
}
