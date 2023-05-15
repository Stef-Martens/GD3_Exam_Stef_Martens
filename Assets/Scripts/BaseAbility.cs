using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseAbility : MonoBehaviour
{
    public virtual void Start()
    {
        ChangeUI(Color.gray, null, "Not available", "Ability needs to be brewed.");
    }
    public virtual void Update() { }
    public abstract int CooldownTime { get; }

    public virtual void ChangeUI(Color color, Sprite sprite, string name, string description)
    {
        GetComponent<WheelMenuItem>().hoverColor = color;
        GetComponent<WheelMenuItem>().baseColor = Color.gray;
        transform.GetChild(1).GetComponent<Image>().sprite = sprite;
        transform.GetChild(2).GetComponent<Text>().text = name;
        transform.GetChild(3).GetComponent<Text>().text = description;
    }

}
