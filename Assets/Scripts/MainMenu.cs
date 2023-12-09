using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstlevel;
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    public void playgame()
    {
        SceneManager.LoadScene(firstlevel);
        Time.timeScale = 1f;
    }   
    public void quitgame()
    {
        Application.Quit();
    }
}
