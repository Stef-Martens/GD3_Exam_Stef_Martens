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
    public GameObject ingredients;
    void Start()
    {
        background.color = baseColor;
        description.SetActive(false);
        title.SetActive(false);
        ingredients.SetActive(false);
    }

    public void Select()
    {
        background.color = hoverColor;
        description.SetActive(true);
        title.SetActive(true);
        ingredients.SetActive(true);
    }

    public void Deselect()
    {
        background.color = baseColor;
        description.SetActive(false);
        title.SetActive(false);
        ingredients.SetActive(false);

    }
}
