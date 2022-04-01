using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public float textWaitTime; // how long it takes for the death screen text and buttons to fade in
    public GameObject deathText;

    private MusicManager musicManagerScript;
    
    void OnEnable() 
    {
        Debug.Log("start death");
        musicManagerScript = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        StartCoroutine(DeathScreen());
    }

    IEnumerator DeathScreen()
    {
        //Sounds.StopAllAudio();
        musicManagerScript.PlayMusic(false);
        yield return new WaitForSeconds(textWaitTime);
        deathText.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        Sounds.PlaySound(Sounds.Sound.Lose_Sound);
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
