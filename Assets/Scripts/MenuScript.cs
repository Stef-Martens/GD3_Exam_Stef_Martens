using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class MenuScript : MonoBehaviour
{
    public Vector2 normalisedMousePosition;
    public float currentAngle;
    public int selection;
    private int previousSelection;

    private WheelMenuItem menuItemSc;
    private WheelMenuItem previousMenuItemSc;
    private StarterAssetsInputs inputs;
    Manager manager;


    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int X, int Y);

    Vector2 previousJoystickValue;

    public GameObject[] menuItems;

    BaseAbility currentAbility;


    void Start()
    {
        manager = FindObjectOfType<Manager>();
        Cursor.lockState = CursorLockMode.Confined;
        inputs = FindObjectOfType<StarterAssetsInputs>();
        currentAbility = FindObjectOfType<Shooting>();
    }
    void Update()
    {
        if (Time.timeScale == 0.05f)
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(true);
            }

            Vector2 joystickValue = Gamepad.current.rightStick.ReadValue();


            if (inputs.look != new Vector2())
            {
                inputs.lookNotNull = inputs.look;
            }


            if (joystickValue.magnitude > 0.7f)
            {
                var test = new Vector2(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2) + inputs.lookNotNull / 2;
                SetCursorPos((int)test.x, (int)test.y);

                normalisedMousePosition = new Vector2(Mouse.current.position.ReadValue().x - Screen.currentResolution.width / 2, Mouse.current.position.ReadValue().y - Screen.currentResolution.height / 2);
                currentAngle = Mathf.Atan2(normalisedMousePosition.y, normalisedMousePosition.x) * Mathf.Rad2Deg;

                currentAngle = (currentAngle + 360) % 360;

                selection = (int)currentAngle / 45;

                if (selection != previousSelection)
                {
                    previousMenuItemSc = menuItems[previousSelection].GetComponent<WheelMenuItem>();
                    previousMenuItemSc.Deselect();
                    previousSelection = selection;

                    menuItemSc = menuItems[previousSelection].GetComponent<WheelMenuItem>();
                    menuItemSc.Select();
                }
            }

            else
            {
                foreach (Transform item in transform)
                {
                    item.gameObject.GetComponent<WheelMenuItem>().Deselect();
                }
            }

        }
        else
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(false);
            }

            foreach (GameObject item in menuItems)
            {
                if (item.GetComponent<WheelMenuItem>().isActive)
                {
                    currentAbility = item.GetComponent<BaseAbility>();
                    currentAbility.Activate();
                    item.GetComponent<WheelMenuItem>().Deselect();
                }
            }
        }

    }

    bool CheckAvailability(int i)
    {
        if (manager.AvailablePowers.Contains(i))
            return true;
        else
        {
            Debug.Log("This skill isn't available yet, brew it in the center when you have all the ingredients!");
            return false;
        }

    }

}
