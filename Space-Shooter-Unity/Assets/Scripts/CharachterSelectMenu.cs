using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharachterSelectMenu : MonoBehaviour
{
    public void HandleStartButtonClicked()
    {
        SceneManager.LoadScene(2);
    }

    void HandleExitButtonPressed()
    {
        Application.Quit();
    }
}
