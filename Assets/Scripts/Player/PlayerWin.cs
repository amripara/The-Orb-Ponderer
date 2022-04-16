using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    public float textWaitTime; // how long it takes for the death screen text and buttons to fade in

    public GameObject winText;
    private MusicManager musicManagerScript;

    void OnEnable() 
    {
        musicManagerScript = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        StartCoroutine(WinScreen());
    }

    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(textWaitTime);
        winText.SetActive(true);
        Sounds.PlaySound(Sounds.Sound.Win_Sound);
    }

    public void ReturnToStartMenu()
    {
        Sounds.PlaySound(Sounds.Sound.Menu_Click);
        musicManagerScript.SetMusic(0);
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Sounds.PlaySound(Sounds.Sound.Menu_Click);
        Application.Quit();
    }

}
