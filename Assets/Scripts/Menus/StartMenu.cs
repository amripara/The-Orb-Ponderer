using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame()
    {
        // TO DO: replace with first gameplay scene
        SceneManager.LoadScene("Level 1");
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    
    public void GoToCreditsMenu()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
