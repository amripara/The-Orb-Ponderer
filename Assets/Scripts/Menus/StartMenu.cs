using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private MusicManager musicManagerScript;
    
    void Awake()
    {
        Sounds.Initialize();
        musicManagerScript = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }
    
    public void StartGame()
    {
        Sounds.PlaySound(Sounds.Sound.Start_Game);
        musicManagerScript.SetMusic(0);
        // TO DO: replace with first gameplay scene
        SceneManager.LoadScene("Tuto");
    }

    public void GoToSettingsMenu()
    {
        Sounds.PlaySound(Sounds.Sound.Menu_Click);
        SceneManager.LoadScene("SettingsMenu");
    }
    
    public void GoToCreditsMenu()
    {
        Sounds.PlaySound(Sounds.Sound.Menu_Click);
        SceneManager.LoadScene("CreditsMenu");
    }

    public void QuitGame()
    {
        Sounds.PlaySound(Sounds.Sound.Player_Death_Grunt1);
        Application.Quit();
    }

    public void ReturnToStartMenu()
    {
        Sounds.PlaySound(Sounds.Sound.Start_Game);
        SceneManager.LoadScene("StartMenu");
    }

    public void StartActualGame()
    {
        Sounds.PlaySound(Sounds.Sound.Start_Game);
        musicManagerScript.SetMusic(1);
        // TO DO: replace with first gameplay scene
        SceneManager.LoadScene("Level 1");
    }
}
