using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button FirstButton;

    public Button PlayButton;
    public Text PlayText;
    public Button QuitButton;
    public Text QuitText;
    EventSystem system;

    void Start()
    {
        FirstButton.Select();
        system = EventSystem.current;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (system.currentSelectedGameObject == PlayButton.gameObject)
        {
            PlayText.color = PlayButton.colors.selectedColor;
            QuitText.color = QuitButton.colors.normalColor;
        }


        else
        {
            PlayText.color = PlayButton.colors.normalColor;
            QuitText.color = QuitButton.colors.selectedColor;
        }
    }
}
