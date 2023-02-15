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
        Time.timeScale = 1.0f;
        SlideOut();
        //Time.timeScale = 1.0f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        SlideIn();
        //transform.LeanMoveLocal(new Vector2(-2754, 300), 1);
        //Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void SlideIn()
    {
        pauseMenuUI.transform.LeanMoveLocalX(1377, 1).setEaseOutQuint();
    }



    public void SlideOut()
    {
        pauseMenuUI.transform.LeanMoveLocalX(2470, 1).setEaseOutQuint();
    }
 
}
