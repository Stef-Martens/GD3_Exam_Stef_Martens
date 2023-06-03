using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnemyTakeover : BaseAbility
{
    public Sprite Image;
    public Color CircleColor;
    public string AbilityName;
    public string Description;
    public override int CooldownTime
    {
        get { return 10; }
    }

    private float currentCooldownTime = 0f;
    private bool isCooldownActive = false;


    public Image cooldownImage;

    private List<GameObject> enemies;  // List of enemies within the detection circle
    private int currentEnemyIndex;      // Index of the currently selected enemy

    bool WasAlreadyPressed = false;

    public override void Start()
    {
        //base.Start();
        enemies = new List<GameObject>();
    }


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
                // mikken
                if (Gamepad.current.leftTrigger.IsPressed() && !isCooldownActive && !WasAlreadyPressed)
                {
                    WasAlreadyPressed = true;
                    //// kiezen van enemy
                    Time.timeScale = 0.1f;

                    Collider[] colliders = Physics.OverlapSphere(FindObjectOfType<ThirdPersonController>().transform.position, 5);
                    enemies = new List<GameObject>();

                    foreach (Collider collider in colliders)
                    {
                        if (collider.GetComponent<BaseEnemy>())
                            enemies.Add(collider.gameObject);
                    }

                    // Set the color of the first enemy
                    if (enemies.Count > 0)
                    {
                        if (enemies[0].tag != "Healer") if (enemies[0]) SetSelectedEnemy(enemies[0]);

                        currentEnemyIndex = 0;
                    }



                }
                else if (Gamepad.current.leftTrigger.IsPressed() && !isCooldownActive && WasAlreadyPressed)
                {
                    if (Gamepad.current.dpad.left.wasPressedThisFrame)
                    {
                        // Switch to the previous enemy
                        int previousIndex = currentEnemyIndex;
                        currentEnemyIndex--;
                        if (currentEnemyIndex < 0)
                            currentEnemyIndex = enemies.Count - 1;

                        ChangeSelectedEnemy(previousIndex, currentEnemyIndex);
                    }
                    else if (Gamepad.current.dpad.right.wasPressedThisFrame)
                    {
                        // Switch to the next enemy
                        int previousIndex = currentEnemyIndex;
                        currentEnemyIndex++;
                        if (currentEnemyIndex >= enemies.Count)
                            currentEnemyIndex = 0;

                        ChangeSelectedEnemy(previousIndex, currentEnemyIndex);
                    }
                }
                else
                {
                    //// stoppen me mikken
                    WasAlreadyPressed = false;




                    if (enemies.Count > 0 && enemies[currentEnemyIndex])
                        SetNotSelectedEnemy(enemies[currentEnemyIndex]);


                    /*foreach (GameObject item in enemies)
                    {
                        if (item)
                            SetNotSelectedEnemy(item);
                    }*/
                }


                if (Gamepad.current.rightTrigger.wasPressedThisFrame && !isCooldownActive && Gamepad.current.leftTrigger.IsPressed())
                {
                    ////// enemy takeover
                    enemies[currentEnemyIndex].GetComponent<BaseEnemy>().Takeover = true;
                    FindObjectOfType<SoundManager>().PlayTakeoverSound();
                    ResetCooldown();
                }


            }
        }
    }


    private void ChangeSelectedEnemy(int previousIndex, int newIndex)
    {
        /* // Change the color of the previously selected enemy back to its original color
         if (previousIndex >= 0 && previousIndex < enemies.Count)
         {
             SetNotSelectedEnemy(enemies[previousIndex]);
         }*/
        foreach (GameObject item in enemies)
        {
            SetNotSelectedEnemy(item);
        }

        // Change the color of the currently selected enemy
        SetSelectedEnemy(enemies[newIndex]);


    }


    private void SetNotSelectedEnemy(GameObject enemy)
    {
        enemy.GetComponent<BaseEnemy>().DeselectTakover();
    }

    private void SetSelectedEnemy(GameObject enemy)
    {
        enemy.GetComponent<BaseEnemy>().SelectTakover();
    }



    void ResetCooldown()
    {

        isCooldownActive = true;
        currentCooldownTime = CooldownTime;
        Debug.Log("Cooldown Started");

    }
}
