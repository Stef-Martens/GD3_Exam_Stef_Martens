using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SuddenDeath : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override int CooldownTime
    {
        get { return 10; }
    }

    public float radius = 5f;

    private bool isCooldownActive = false;
    private float currentCooldownTime = 0f;

    public Image cooldownImage;


    public override void Update()
    {
        if (!FindObjectOfType<Manager>().PauseMenu.activeSelf)
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
                }
            }

            if (FindObjectOfType<MenuScript>().currentAbility == this)
            {
                cooldownImage.fillAmount = currentCooldownTime / CooldownTime;
                if (Gamepad.current.rightTrigger.wasPressedThisFrame && !isCooldownActive)
                {
                    Collider[] hitColliders = Physics.OverlapSphere(FindObjectOfType<ThirdPersonController>().transform.position, radius);

                    foreach (Collider col in hitColliders)
                    {

                        if (col.GetComponent<BaseEnemy>())
                        {
                            Destroy(col.gameObject);
                        }

                    }
                    FindObjectOfType<SoundManager>().PlayShockwaveSound();
                    ResetCooldown();
                }
            }
        }
    }


    void ResetCooldown()
    {
        isCooldownActive = true;
        currentCooldownTime = CooldownTime;
    }



    public override void Start()
    {

    }
}
