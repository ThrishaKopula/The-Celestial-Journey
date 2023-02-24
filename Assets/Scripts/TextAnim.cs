using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAnim : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] float timeBtwnChars;
    [SerializeField] float timeBtwnWords;

    public string[] stringArray;
    public int[] starLines = {1};
    public int[] venusLines;
    public GameObject starAvatar;
    public GameObject venusAvatar;
    public GameObject dialogBox;
    public GameObject pauseMenu;
    public bool isStarVisible = false;
    public bool isVenusVisible = false;
    public static bool isDialogue;
    public GameObject backOpacityScreen;
    public GameObject dialogueScreen;
    int i = 0;
    void Start()
    {
        isDialogue = true;
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        dialogBox.transform.LeanMoveLocalY(0, 0.7f).setEaseOutQuint().setIgnoreTimeScale(true);
        backOpacityScreen.SetActive(true);
        CheckCharacter();
        _textMeshPro.text = stringArray[i];
        StartCoroutine(TextVisible());
    }

    
    private void Update()
    {
        if(PauseMenu.gameIsPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                EndCheck();
            }
        }
       
    }

    public void EndCheck()
    {
        if(i <= stringArray.Length - 1)
        {
            CheckCharacter();
            _textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisible());
        } else
        {
            EndDialogueScreen();
        }
    }
    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleCharacters = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        while(true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleCharacters)
            {
                i += 1;
                break;
            }
            counter += 1;
            yield return new WaitForSecondsRealtime(timeBtwnChars); 
        }
    }

    void CheckCharacter()
    {
        if (System.Array.IndexOf(starLines, i) > -1)
        {
            if(isVenusVisible == true)
            {
                SlideOutLeft(venusAvatar);
                isVenusVisible = false; 
            }
            SlideInLeft(starAvatar);
            isStarVisible = true;
        }
        else if (System.Array.IndexOf(venusLines, i) > -1)
        {

            if (isStarVisible == true)
            {
                SlideOutLeft(starAvatar);
                isStarVisible = false;
            }
            SlideInLeft(venusAvatar);
            isVenusVisible=true;
        }
    }

    void SlideInLeft(GameObject character)
    {
        character.transform.LeanMoveLocalX(-1450, 0.7f).setEaseOutQuint().setIgnoreTimeScale(true);
    }

    void SlideOutLeft(GameObject character)
    {
        character.transform.LeanMoveLocalX(-3200, 0.7f).setEaseOutQuint().setIgnoreTimeScale(true);
    }

    void EndDialogueScreen()
    {
        Time.timeScale = 1.0f;
        backOpacityScreen.SetActive(false);
        dialogBox.transform.LeanMoveLocalY(-1200, 0.7f).setEaseOutQuint().setIgnoreTimeScale(true);
        pauseMenu.SetActive(true);
        isDialogue = false;
        if (isStarVisible)
        {
            SlideOutLeft(starAvatar);
        }
        if (isVenusVisible)
        {
            SlideOutLeft(venusAvatar);
        }
    }
}


