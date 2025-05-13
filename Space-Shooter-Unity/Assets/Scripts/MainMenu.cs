using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button goButton;

    public GameObject mainContainer;
    public GameObject characterContainer;

    void Start()
    {
        startButton.onClick.AddListener(HandleStartPressed);
        goButton.onClick.AddListener(HandleGoButtonClicked);
    }

    void HandleStartPressed()
    { 
        mainContainer.SetActive(false);
        characterContainer.SetActive(true);
    }

    void HandleGoButtonClicked()
    {
        SceneManager.LoadScene(1);
        SoundsManager.Instance.PlayBGM(SoundsManager.Instance.source17);
    }

    void HandleExitButtonPressed()
    { 
        Application.Quit();
    }
}
