using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public string mainMenuScene;

    void Start()
    {
        
    }



    void Update()
    {
        
    }

    public void Resume()
    {
        GameManager.Instance.PauseUnpause();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
