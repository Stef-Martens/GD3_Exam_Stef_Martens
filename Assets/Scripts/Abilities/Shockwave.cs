using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Shockwave : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override int CooldownTime
    {
        get { return 3; }
    }

    public float force = 10f;
    public float radius = 10f;

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
                            Rigidbody rb = col.gameObject.GetComponent<Rigidbody>();

                            if (rb != null)
                            {
                                Vector3 direction = rb.transform.position - FindObjectOfType<ThirdPersonController>().transform.position;
                                float distance = direction.magnitude;

                                float falloff = 1 - Mathf.Clamp01(distance / radius);
                                Vector3 forceVector = direction.normalized * force * falloff;

                                rb.AddForce(forceVector, ForceMode.Impulse);
                            }
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
