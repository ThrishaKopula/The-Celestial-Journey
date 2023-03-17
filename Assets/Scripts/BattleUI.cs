using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{

    public GameObject starActiveIcon;
    public GameObject venusActiveIcon;

    //char 1
    private bool switch1 = true;
    //char 2
    private bool switch2 = true;
    //char 3
    //private bool switch3 = true;
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
            if (switch1) {
                StartCoroutine(SwapCooldown(1));
                starActiveIcon.SetActive(true);
                venusActiveIcon.SetActive(false);
            }
        }

        //swap to 1
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (switch2) {
                StartCoroutine(SwapCooldown(0));
                starActiveIcon.SetActive(false);
                venusActiveIcon.SetActive(true);
            }
        }
    }

    IEnumerator SwapCooldown(int index) {
        if (index == 0) {
            switch1 = false;
            yield return new WaitForSeconds(5f);
            switch1 = true;
        } else if (index == 1) {
            switch2 = false;
            yield return new WaitForSeconds(5f);
            switch2 = true;
        } //else if (index == 2) {
        //     switch3 = false;
        //     yield return new WaitForSeconds(5f);
        //     switch3 = true;
        // }
    }
}
