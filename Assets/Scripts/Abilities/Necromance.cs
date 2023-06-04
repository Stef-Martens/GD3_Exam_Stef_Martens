using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Necromance : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override float CooldownTime
    {
        get { return 10; }
    }

    private bool isCooldownActive = false;
    private float currentCooldownTime = 0f;

    public Image cooldownImage;

    public GameObject DwarfPrefab;


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
                    SpawnSkeletons();
                    FindObjectOfType<SoundManager>().PlayNecromanceSound();
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

    void SpawnSkeletons()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle * 3;
            Vector3 spawnPos = new Vector3(randomPos.x, 0.5f, randomPos.y) + FindObjectOfType<ThirdPersonController>().transform.position;
            GameObject dwarf = Instantiate(DwarfPrefab, spawnPos, Quaternion.identity);
            dwarf.GetComponent<Dwarf>().Takeover = true;
        }
    }
}