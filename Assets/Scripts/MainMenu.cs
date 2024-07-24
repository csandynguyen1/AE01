using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Home_Level");
    }
    public void QuitGame() 
    {
        Debug.Log("Game closed");
        Application.Quit();
    }
}
