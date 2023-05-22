using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Freeze : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override int CooldownTime
    {
        get { return 3; }
    }

    private float currentCooldownTime = 0f;

    public float freezeDuration = 5f;
    public float radius = 10f;
    private bool isCooldownActive = false;

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
                    Debug.Log("Cooldown Finished");
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
                            col.GetComponent<BaseEnemy>().Frozen = true;
                            StartCoroutine(UnfreezeEnemiesAfterDelay(col.GetComponent<BaseEnemy>()));
                        }

                    }
                    FindObjectOfType<SoundManager>().PlayFreezeSound();
                    ResetCooldown();

                }
            }
        }


    }

    void ResetCooldown()
    {

        isCooldownActive = true;
        currentCooldownTime = CooldownTime;
        Debug.Log("Cooldown Started");

    }

    IEnumerator UnfreezeEnemiesAfterDelay(BaseEnemy enemy)
    {
        yield return new WaitForSeconds(freezeDuration);
        enemy.Frozen = false;
    }


    public override void Start()
    {

    }
}
