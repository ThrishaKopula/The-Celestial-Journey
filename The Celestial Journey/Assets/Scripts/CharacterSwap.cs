using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    public Transform character;
    public List<Transform> possibleChars;
    public int whichCharacter;
    // Start is called before the first frame update
    void Start()
    {
        if (character == null && possibleChars.Count >= 1) {
            character = possibleChars[0];
        }
        Swap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            if (whichCharacter == 0) {
                whichCharacter = possibleChars.Count - 1;
            } else {
                whichCharacter -= 1;
            }
            Swap();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (whichCharacter == possibleChars.Count - 1) {
                whichCharacter = 0;
            } else {
                whichCharacter += 1;
            }
            Swap();
        }
    }

    public void Swap() {
        character = possibleChars[whichCharacter];
        character.GetComponent<ThirdPersonMovement>().enabled = true;
        for (int i = 0; i < possibleChars.Count; i++) {
            if (possibleChars[i] != character) {
                possibleChars[i].GetComponent<ThirdPersonMovement>().enabled = false;
            }
        }
    }
}
