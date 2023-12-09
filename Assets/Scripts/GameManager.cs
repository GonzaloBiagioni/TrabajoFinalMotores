using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public float waitAfterDying = 2f;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            PauseUnpause();
        }
    }

    public void PlayerDied()
    {
        StartCoroutine("playerDiedCo");
    }

    public IEnumerator playerDiedCo()
    {
        yield return new WaitForSeconds(waitAfterDying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseUnpause()
    {
        if(UIController.Instance.pauseScreen.activeInHierarchy)
        {
            UIController.Instance.pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            PlayerController.Instance.footStep.Play();
        }
        else
        {
            UIController.Instance.pauseScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = false;
            Time.timeScale = 0f;
            PlayerController.Instance.footStep.Stop();
        }
    }    
}
