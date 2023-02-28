using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsScrollView : MonoBehaviour
{
    public GameObject scrollObj;
    public int totalViews = 2;
    public int currentView = 1;
    public GameObject rightButton;
    public GameObject leftButton;
    // Start is called before the first frame update
    void Start()
    {
        rightButton.SetActive(true);
        leftButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentView == totalViews)
        {
            rightButton.SetActive(false);
        } else
        {
            rightButton.SetActive(true);
        }
        if(currentView == 1)
        {
            leftButton.SetActive(false);
        } else
        {
            leftButton.SetActive(true);
        }
    }

    public void GoRight()
    {
        scrollObj.transform.LeanMoveLocalX(-3840, 0.7f).setEaseOutQuint().setIgnoreTimeScale(true);
        currentView += 1;
    }

    public void GoLeft()
    {
        scrollObj.transform.LeanMoveLocalX(0, 0.7f).setEaseOutQuint().setIgnoreTimeScale(true);
        currentView -= 1;
    }
}
