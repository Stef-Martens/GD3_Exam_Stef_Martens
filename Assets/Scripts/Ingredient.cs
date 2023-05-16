using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ingredient : MonoBehaviour
{
    public string[] IngredientName;
    bool CanTake = false;

    // Update is called once per frame
    void Update()
    {
        if (CanTake && Gamepad.current.xButton.wasPressedThisFrame)
        {
            foreach (var item in IngredientName)
            {
                FindObjectOfType<Manager>().Ingredients.Add(item);
                FindObjectOfType<Cauldron>().ControlsText.text = "";
            }
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Cauldron>().ControlsText.text = "Press X to pick up ingredients.";
            CanTake = true;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Cauldron>().ControlsText.text = "";
            CanTake = false;
        }
    }
}
