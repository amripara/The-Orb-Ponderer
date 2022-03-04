using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerWin : MonoBehaviour
{
    public float textWaitTime; // how long it takes for the death screen text and buttons to fade in

    public GameObject winText;
    
    void OnEnable() 
    {
        StartCoroutine(WinScreen());
    }

    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(textWaitTime);
        winText.SetActive(true);
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
