using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override int CooldownTime
    {
        get { return 3; }
    }


    public override void Update()
    {
        if (FindObjectOfType<Manager>().Inventory.Contains(this))
        {
            ChangeUI(CircleColor, Image, AbilityName, Description);
        }

        if (FindObjectOfType<MenuScript>().currentAbility == this)
        {






        }

    }


    public override void Activate()
    {

    }

    public override void Start()
    {

    }
}
