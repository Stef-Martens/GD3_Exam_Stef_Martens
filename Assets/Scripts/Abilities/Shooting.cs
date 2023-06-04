using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shooting : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override float CooldownTime
    {
        get { return 0.15f; }
    }

    public LineRenderer lineRenderer;
    public GameObject Player;
    public GameObject Bullet;


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

                if (Gamepad.current.leftTrigger.IsPressed())
                {
                    lineRenderer.enabled = true;
                    lineRenderer.SetPosition(0, Player.transform.position + new Vector3(0, 1, 0)); // set the starting position of the line to the player's position
                    lineRenderer.SetPosition(1, Player.transform.position + new Vector3(0, 1, 0) + Player.transform.forward * 10); // set the ending position of the line to the player's position + forward direction
                }
                else
                {
                    lineRenderer.enabled = false;
                }




                if (Gamepad.current.rightTrigger.wasPressedThisFrame && !isCooldownActive)
                {
                    FindObjectOfType<SoundManager>().PlayShootSound();
                    GameObject projectile = Instantiate(Bullet, Player.transform.position + new Vector3(0, 0.5f, 0) + Player.transform.forward, Player.transform.rotation);
                    Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
                    projectileRigidbody.velocity = Player.transform.forward * 20;

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
        ChangeUI(CircleColor, Image, AbilityName, Description);
        lineRenderer = FindObjectOfType<LineRenderer>();
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        Player = FindObjectOfType<CharacterController>().gameObject;
    }
}
