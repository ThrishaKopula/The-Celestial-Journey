using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject backOpacityScreen;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        SlideOut();
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(true);
        backOpacityScreen.SetActive(false);
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        SlideIn();
        Time.timeScale = 0f;
        gameIsPaused = true;
        backOpacityScreen.SetActive(true);
    }

    public void SlideIn()
    {
        pauseMenuUI.transform.LeanMoveLocalX(1333, 0.7f).setEaseOutQuint().setIgnoreTimeScale(true);
    }
    //1143

    public void SlideOut()
    {
        pauseMenuUI.transform.LeanMoveLocalX(2709, 0.7f).setEaseOutQuint().setIgnoreTimeScale(true);
    }

    public void LoadMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(0);
    }

}
