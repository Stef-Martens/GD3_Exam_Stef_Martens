using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override int CooldownTime
    {
        get { return 0; }
    }

    public override void Activate()
    {

    }

    public override void Update()
    {
        if (FindObjectOfType<MenuScript>().currentAbility == this)
        {






        }

    }

    public override void Start()
    {
        ChangeUI(CircleColor, Image, AbilityName, Description);
    }
}
