using System.Collections;
using System.Collections.Generic;
using FischlWorks_FogWar;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FireComet : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override int CooldownTime
    {
        get { return 5; }
    }

    private float currentCooldownTime = 0f;
    private bool isCooldownActive = false;

    private Vector2 movementInput;

    public GameObject AimObject;
    public GameObject FirePrefab;

    public Image cooldownImage;



    public override void Update()
    {

        if (FindObjectOfType<Manager>().Inventory.Contains(this))
        {
            ChangeUI(CircleColor, Image, AbilityName, Description);
        }

        if (isCooldownActive)
        {
            currentCooldownTime -= Time.deltaTime;

            if (currentCooldownTime <= 0)
            {
                isCooldownActive = false;
                currentCooldownTime = 0;
                Debug.Log("Cooldown Finished");
            }
        }

        if (FindObjectOfType<MenuScript>().currentAbility == this)
        {
            cooldownImage.fillAmount = currentCooldownTime / CooldownTime;
            // mikken
            if (Gamepad.current.leftTrigger.IsPressed() && !isCooldownActive)
            {
                AimObject.SetActive(true);
                Time.timeScale = 0.1f;


                Vector3 direction = new Vector3(movementInput.x, 0f, movementInput.y);
                AimObject.transform.position += direction * 3 * Time.fixedDeltaTime;
            }
            else
            {
                AimObject.SetActive(false);
                AimObject.transform.position = FindObjectOfType<ThirdPersonController>().transform.position;
            }


            // schieten
            if (Gamepad.current.rightTrigger.wasPressedThisFrame && !isCooldownActive && Gamepad.current.leftTrigger.IsPressed())
            {
                // spawn fireball
                GameObject fireball = Instantiate(FirePrefab, FindObjectOfType<ThirdPersonController>().transform.position + new Vector3(0, 10, 0), Quaternion.identity);
                int index = FindObjectOfType<csFogWar>().AddFogRevealer(new csFogWar.FogRevealer(fireball.transform, 3, true));
                fireball.GetComponent<Fireball>().Destination = AimObject.transform.position;
                fireball.GetComponent<Fireball>().IndexRevealer = index;

                AimObject.SetActive(false);
                AimObject.transform.position = FindObjectOfType<ThirdPersonController>().transform.position;
                ResetCooldown();
            }


        }
    }

    private void FixedUpdate()
    {
        if (Gamepad.current != null)
        {
            movementInput = Gamepad.current.rightStick.ReadValue();
        }
    }




    void ResetCooldown()
    {

        isCooldownActive = true;
        currentCooldownTime = CooldownTime;
        Debug.Log("Cooldown Started");

    }


    public override void Start()
    {
        AimObject.transform.position = FindObjectOfType<ThirdPersonController>().transform.position;
    }
}
