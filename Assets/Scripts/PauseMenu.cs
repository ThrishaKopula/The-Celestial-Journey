using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

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

    void ResumeGame()
    {
        SlideOut();
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(true);
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        SlideIn();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void SlideIn()
    {
        pauseMenuUI.transform.LeanMoveLocalX(1143, 1).setEaseOutQuint().setIgnoreTimeScale(true);
    }

    public void SlideOut()
    {
        pauseMenuUI.transform.LeanMoveLocalX(2709, 1).setEaseOutQuint().setIgnoreTimeScale(true);
    }
 
}
