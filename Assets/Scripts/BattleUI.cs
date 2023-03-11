using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{

    public GameObject starActiveIcon;
    public GameObject venusActiveIcon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //swap to 0
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            starActiveIcon.SetActive(true);
            venusActiveIcon.SetActive(false);
        }

        //swap to 1
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            starActiveIcon.SetActive(false);
            venusActiveIcon.SetActive(true);
        }
    }
}
