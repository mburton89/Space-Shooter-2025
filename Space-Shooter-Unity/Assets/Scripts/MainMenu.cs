using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    void Start()
    {
        startButton.onClick.AddListener(HandleStartButtonClicked);
    }

    void HandleStartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    void HandleExitButtonPressed()
    { 
        Application.Quit();
    }
}
