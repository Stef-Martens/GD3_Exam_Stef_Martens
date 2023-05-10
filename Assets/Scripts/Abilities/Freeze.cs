using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override int CooldownTime
    {
        get { return 3; }
    }

    public bool Available;

    public override void Update()
    {
        if (Available)
        {
            ChangeUI(CircleColor, Image, AbilityName, Description);
            Available = false;
        }
    }


    public override void Activate()
    {

    }

    public override void Start()
    {

    }
}
