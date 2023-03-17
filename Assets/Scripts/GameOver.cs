using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject levelChange;

    public void ReturnToMain()
    {
        levelChange.GetComponent<LevelChanger>().FadeToLevel(0);
    }

    public void Continue()
    {

    }
}
