using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    public string mainMenuScene;
    public float timeBetweenShowing = 1f;
    public GameObject textBox, returnButton;
    public Image blackScreen;
    public float blackScreenfade = 2f;

    void Start()
    {
        StartCoroutine("ShowObjectsCo");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    
    void Update()
    {
        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, blackScreenfade * Time.deltaTime));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public IEnumerator ShowObjectsCo()
    {
        yield return new WaitForSeconds(timeBetweenShowing);

        textBox.SetActive(true);

        yield return new WaitForSeconds(timeBetweenShowing);

        returnButton.SetActive(true);
    }
}
