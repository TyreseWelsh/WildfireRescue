using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    //Main Menu
    public void clickPlayButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void clickControlsButton()
    {
        SceneManager.LoadScene("Controls");
    }

    public void clickQuitButton()
    {
        Application.Quit();
    }


    //End of level screen
    public void clickNextLevelButton()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else
        {
            SceneManager.LoadScene("NoMoreLevels");
        }
    }

    public void clickQuitToMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }


    //Pause Menu
    public void clickRestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
