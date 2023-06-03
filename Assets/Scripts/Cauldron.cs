using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Cauldron : MonoBehaviour
{
    public Text ControlsText;
    bool canOpen = false;

    public bool MenuIsOpen = false;

    public GameObject MenuUI;

    public Selectable FirstInput;
    EventSystem system;

    public Image CurrentImage;
    public Text CurrentAbility;
    public Text Ingredients;
    public Text InformationText;


    void Start()
    {
        system = EventSystem.current;
    }

    void Update()
    {
        if (canOpen && Gamepad.current.xButton.wasPressedThisFrame && !FindObjectOfType<Manager>().PauseMenu.activeSelf)
        {
            MenuIsOpen = true;
            MenuUI.SetActive(true);
            FirstInput.Select();
            ControlsText.text = "Press B to close brewing menu.";
        }
        if (MenuIsOpen && Gamepad.current.bButton.wasPressedThisFrame)
        {
            CloseMenu();
        }

        if (MenuIsOpen)
        {
            CurrentImage.sprite = system.currentSelectedGameObject.gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
            CurrentAbility.text = system.currentSelectedGameObject.gameObject.name;
            Ingredients.text = "";
            foreach (var item in system.currentSelectedGameObject.GetComponent<ItemCauldron>().IngredientsNames)
            {
                if (FindObjectOfType<Manager>().Ingredients.Contains(item))
                    Ingredients.text = Ingredients.text + Environment.NewLine + "<color=green>" + item + "</color>";
                else
                    Ingredients.text = Ingredients.text + Environment.NewLine + "<color=red>" + item + "</color>";
            }
            InformationText.text = system.currentSelectedGameObject.GetComponent<ItemCauldron>().Info;
        }



    }

    public void CloseMenu()
    {
        MenuIsOpen = false;
        MenuUI.SetActive(false);
        ControlsText.text = "Press X to open brewing menu.";
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControlsText.text = "Press X to open brewing menu.";
            canOpen = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControlsText.text = "";
            canOpen = false;
        }
    }
}
