using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public float textWaitTime; // how long it takes for the death screen text and buttons to fade in

    public GameObject deathText;
    
    void OnEnable() 
    {
        Debug.Log("start death");
        StartCoroutine(DeathScreen());
    }

    IEnumerator DeathScreen()
    {
        yield return new WaitForSeconds(textWaitTime);
        deathText.SetActive(true);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

}
