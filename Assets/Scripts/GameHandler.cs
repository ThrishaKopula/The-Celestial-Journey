using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject character;

    //A list of prefabs of the characters. This is possible since we will be keeping the information that
    //has to be persistent in this script.
    public List<GameObject> CharacterList;

    //A dictionary of the character names, and their health value. Since each character will have a
    //unique name, we can always know how much health a character has, and we can always access their values.
    public Dictionary<string, float> CharacterHealths;

    //
    public int whichCharacter;

    // Start is called before the first frame update
    void Start()
    {
        //setting character to the first character on startup
        if (character == null && CharacterList.Count >= 1) {
            character = Instantiate(CharacterList[0], this.transform.position, this.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //going left through the list
        if (Input.GetKeyDown(KeyCode.Z)){
            PreviousCharacter();
        }
        //going right through the list
        if (Input.GetKeyDown(KeyCode.X)){
            NextCharacter();
        }
        
        //swap to 0
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            Swap(0);
        }

        //swap to 1
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Swap(1);
        }
    }
    //Health interaction functions
    public float GetHealth(){
        //return CharacterHealths[character.GetComponent<Character>().name];
        return 0;
    }
    public float GetHealth(string charName){
        return CharacterHealths[charName];
    }


    //damage to deal to the player
    public void Damage(int damage){
        // CharacterHealths[character.GetComponent<Character>().name] -= damage;
        // if (CharacterHealths[character.GetComponent<Character>().name] < 0){
        //     //character died / incapacitated
        // }
    }
    public void Damage(int damage, string charName){
        CharacterHealths[charName] -= damage;
        if (CharacterHealths[charName] < 0){
            //character died / incapacitated
        }
    }

    //Character swapping functions
    public void PreviousCharacter(){
        //decrement the whichCharacter, with wraparound
        if (whichCharacter == 0) {
            whichCharacter = CharacterList.Count - 1;
        } else {
            whichCharacter -= 1;
        }
    }
    public void NextCharacter(){
        //increment the whichCharacter, with wraparound
        if (whichCharacter == CharacterList.Count - 1) {
            whichCharacter = 0;
        } else {
            whichCharacter += 1;
        }
    }

    public void Swap(int index) {
        if (index < 0 || index > CharacterList.Count - 1) return;
        whichCharacter = index;
        //kep track of where the player currently is
        Vector3 position = character.transform.position;
        if (character != null) Destroy(character);
        character = Instantiate(CharacterList[whichCharacter]);
        //character.GetComponent<ThirdPersonMovement>().enabled = true;
        for (int i = 0; i < CharacterList.Count; i++) {
            if (CharacterList[i] != character) {
                //CharacterList[i].GetComponent<ThirdPersonMovement>().enabled = false;
            }
        }
    }
}
