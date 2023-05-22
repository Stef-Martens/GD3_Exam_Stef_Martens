using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public List<BaseAbility> Inventory;

    public float Health = 200f;

    public Text HealthText;

    Cauldron cauldron;

    public List<string> Ingredients;

    Map map;

    public GameObject GameOver;
    public Selectable FirstButtonGameOver;
    public Text ReasonGameOver;

    public GameObject PauseMenu;
    public Selectable FirstSelectedPause;

    public AudioSource Backgroundmusic;

    public List<AudioSource> SoundEffects;

    public int Days = 0;
    public Text DaysText;

    public GameObject[] EnemiesPrefabs;

    public List<GameObject> EnemieAtCauldron;

    public Text BaseAttackedText;

    void Start()
    {
        Inventory = new List<BaseAbility>();
        Inventory.Add(FindObjectOfType<Shooting>());
        /*Inventory.Add(FindObjectOfType<Freeze>());
        Inventory.Add(FindObjectOfType<Shockwave>());
        Inventory.Add(FindObjectOfType<Necromance>());
        Inventory.Add(FindObjectOfType<FireComet>());*/
        cauldron = FindObjectOfType<Cauldron>();
        map = FindObjectOfType<Map>();
        SoundEffects = new List<AudioSource>();
        foreach (AudioSource item in FindObjectsOfType<AudioSource>())
        {
            if (item != Backgroundmusic)
            {
                SoundEffects.Add(item);
            }
        }

        StartCoroutine(IncreaseDays());

        EnemieAtCauldron = new List<GameObject>();
    }

    public void StartGameOver(string reason)
    {
        GameOver.SetActive(true);
        FirstButtonGameOver.Select();
        ReasonGameOver.text = reason;
        Time.timeScale = 0f;
    }

    public void OpenPause()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        FirstSelectedPause.Select();
    }

    public void ClosePause()
    {
        PauseMenu.SetActive(false);
    }

    public void ChangeVolumeMusic(Slider slider)
    {
        Backgroundmusic.volume = slider.value;
    }

    public void ChangeVolumeEffects(Slider slider)
    {

        foreach (var source in SoundEffects)
        {
            source.volume = slider.value;
        }
    }


    void Update()
    {
        if (Health <= 0 && !GameOver.activeSelf)
        {
            StartGameOver("You died!");
        }

        DaysText.text = Days.ToString();


        HealthText.text = Health.ToString();

        if (Gamepad.current.leftShoulder.IsPressed() && !cauldron.MenuIsOpen && !map.IsOpen && !PauseMenu.activeSelf)
        {
            Time.timeScale = 0.05f;
        }
        else if (cauldron.MenuIsOpen && !GameOver.activeSelf && !PauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else if (Gamepad.current.startButton.wasPressedThisFrame && !cauldron.MenuIsOpen && !map.IsOpen && !PauseMenu.activeSelf)
        {
            OpenPause();
        }
        else if (PauseMenu.activeSelf && (Gamepad.current.startButton.wasPressedThisFrame || Gamepad.current.buttonEast.wasPressedThisFrame))
        {
            ClosePause();
        }
        else if (!GameOver.activeSelf && !PauseMenu.activeSelf)
        {
            Time.timeScale = 1f;
        }


        if (Gamepad.current.selectButton.wasPressedThisFrame && !cauldron.MenuIsOpen && !Gamepad.current.leftShoulder.IsPressed() && !map.IsOpen)
            map.IsOpen = true;
        else if (Gamepad.current.selectButton.wasPressedThisFrame && !cauldron.MenuIsOpen && !Gamepad.current.leftShoulder.IsPressed() && map.IsOpen)
            map.IsOpen = false;
    }

    public void AddInventory(BaseAbility i)
    {
        Inventory.Add(i);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartAttack()
    {
        // Calculate the angle between each object
        float angleStep = 360f / 8;

        int enemiesCount = 0;

        // Spawn objects in a circle
        for (int i = 0; i < 8; i++)
        {
            // Calculate the position around the circle
            float angle = i * angleStep;
            Vector3 spawnPosition = cauldron.transform.position + Quaternion.Euler(0f, angle, 0f) * Vector3.forward * 4;


            if (enemiesCount <= 4)
                EnemieAtCauldron.Add(Instantiate(EnemiesPrefabs[0], spawnPosition, transform.rotation));

            else if (enemiesCount <= 7)
                EnemieAtCauldron.Add(Instantiate(EnemiesPrefabs[1], spawnPosition, transform.rotation));
            else
                EnemieAtCauldron.Add(Instantiate(EnemiesPrefabs[2], spawnPosition, transform.rotation));

            enemiesCount++;
        }

        StartCoroutine(BlinkTextRoutine());
    }

    private IEnumerator BlinkTextRoutine()
    {
        for (int i = 0; i <= 6; i++)
        {
            // Toggle the visibility of the text
            BaseAttackedText.enabled = !BaseAttackedText.enabled;

            // Wait for the specified blink interval
            yield return new WaitForSeconds(0.2f);
        }

        // Ensure the text is visible after blinking
        BaseAttackedText.enabled = false;
    }

    private IEnumerator IncreaseDays()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);

            Days++;

            if (Days % 5 == 0)
            {
                StartAttack();
            }
        }
    }
}
