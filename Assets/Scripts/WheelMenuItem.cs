using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelMenuItem : MonoBehaviour
{
    public Color hoverColor;
    public Color baseColor;
    public Image background;
    public GameObject description;
    public GameObject title;

    public bool isActive = false;
    void Start()
    {
        if (GetComponent<Shooting>())
            FindObjectOfType<MenuScript>().currentAbility = GetComponent<Shooting>();


        background.color = baseColor;
        description.SetActive(false);
        title.SetActive(false);
    }

    public void Select()
    {
        isActive = true;
        background.color = hoverColor;
        description.SetActive(true);
        title.SetActive(true);
    }

    public void Deselect()
    {
        isActive = false;
        background.color = baseColor;
        description.SetActive(false);
        title.SetActive(false);
    }
}
