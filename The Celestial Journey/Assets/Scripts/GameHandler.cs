using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject character;
    public CameraMovement cameraMovement;

    //A list of prefabs of the characters. This is possible since we will be keeping the information that
    //has to be persistent in this script.
    public List<GameObject> CharacterList;

    //A dictionary of the character names, and their health value. Since each character will have a
    //unique name, we can always know how much health a character has, and we can always access their values.
    public Dictionary<string, float> CharacterHealths;

    //current character
    int whichCharacter;

    //char 1
    private bool switch1 = true;
    //char 2
    private bool switch2 = true;
    //char 3
    private bool switch3 = true;

    //particle system
    public ParticleSystem m_ParticleSystem;

    public List<GameObject> roomList;
    int whichRoom = 0;
    Room room;

    // Start is called before the first frame update
    void Start()
    {
        //setting character to the first character on startup
        if (character == null && CharacterList.Count >= 1) {
            character = Instantiate(CharacterList[0], this.transform.position, this.transform.rotation);
        }
<<<<<<< Updated upstream:The Celestial Journey/Assets/Scripts/GameHandler.cs
        if (CharacterList.Count >= 1) {
            for (int i = 0; i < CharacterList.Count; i++) {
                if (i != whichCharacter) {
                   CharacterList[i].SetActive(false); 
                }
            }
        }
=======
        //initialize the dictionary of healths, 
>>>>>>> Stashed changes:Assets/Scripts/GameHandler.cs
        CharacterHealths = new Dictionary<string, float>();
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
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (switch1) {
                StartCoroutine(SwapCooldown(whichCharacter));
                Swap(0);
            }
        }

        //swap to 1 if possible
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (CharacterList.Count >= 1 && switch2) {
                StartCoroutine(SwapCooldown(whichCharacter));
                Swap(1);
            }
        }

        //swap to 2 if possible
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (CharacterList.Count >= 2 && switch3) {
                StartCoroutine(SwapCooldown(whichCharacter));
                Swap(2);
            }
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
    public void Damage(float damage){
        
        CharacterHealths.TryAdd(character.GetComponent<Character>().name, 10);
        CharacterHealths[character.GetComponent<Character>().name] -= damage;
        //Debug.Log("Enemy did damage!");
        if (CharacterHealths[character.GetComponent<Character>().name] < 0){
            //Debug.Log("Character died!");
            //character died / incapacitated
        }
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
        //Debug.Log("prev");
        int temp = whichCharacter;
        if (whichCharacter == 0) {
            whichCharacter = CharacterList.Count - 1;
        } else {
            whichCharacter -= 1;
        }
        if (temp == whichCharacter) return; // swapping to the same character
        Swap(whichCharacter);
    }
    public void NextCharacter(){
        //increment the whichCharacter, with wraparound
        //Debug.Log("next");
        int temp = whichCharacter;
        if (whichCharacter == CharacterList.Count - 1) {
            whichCharacter = 0;
        } else {
            whichCharacter += 1;
        }
        if (temp == whichCharacter) return; // swapping to the same character
        //Debug.Log("next is " + whichCharacter);
        Swap(whichCharacter);
    }

    public void Swap(int index) {
        if (index < 0 || index > CharacterList.Count - 1) return; // bounds check
        if (whichCharacter == index) return; // swapping to the same character
        whichCharacter = index;
        //keep track of where the player and rotation currently is
        Vector3 position = character.transform.position;
        Quaternion rotation = character.transform.rotation;
        if (character != null) Destroy(character);
        character = Instantiate(CharacterList[whichCharacter], position, rotation);
        cameraMovement.PlayerTransform = character.transform;
        m_ParticleSystem.transform.position = position;
        m_ParticleSystem.Play();
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
        } else if (index == 2) {
            switch3 = false;
            yield return new WaitForSeconds(5f);
            switch3 = true;
        }
    }
    
    public void nextRoom(){
        unloadRoom();
        whichRoom++;
        loadRoom(whichRoom);
    }

    public void loadRoom(int index){
        if (index >= roomList.Count) return;
        //create the prefab of the room in the game world
        GameObject roomObject = Instantiate(roomList[index], Vector3.zero, this.transform.rotation);
        //get the room script to find locations of the player spawn
        room = roomObject.GetComponent<Room>();
        //move the player to the spawn position
        character.transform.position = room.playerSpawnLocation.transform.position;
    }

    public void unloadRoom(){
        if (room != null)
        Destroy(room.gameObject);
    }
}
