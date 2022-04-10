using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gui;
    // Start is called before the first frame update
    private MusicManager musicManagerScript;
    void Start()
    {
        musicManagerScript = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapGUI(bool isPaused)
    {
        if (gui != null && pauseMenu != null)
        {
            if (isPaused)
            {
                pauseMenu.SetActive(true);
                gui.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(false);
                gui.SetActive(true);
            }
        }
    }
    public void ReloadLevel()
    {
        Sounds.PlaySound(Sounds.Sound.Start_Game);
        musicManagerScript.SetMusic(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToStartMenu()
    {
        Sounds.PlaySound(Sounds.Sound.Menu_Click);
        musicManagerScript.SetMusic(0);
        SceneManager.LoadScene("StartMenu");
    }
}
