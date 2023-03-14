using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Dictionary<string, float> CharacterUlts;

    //current character
    int whichCharacter;

    //particle system
    public ParticleSystem m_ParticleSystem;

    public List<GameObject> roomList;
    int whichRoom = 0;
    Room room;

    //Health Bar and Ult Bar
    public Slider healthBar;
    public Slider ultBar;
    private float lerpSpeed = 0.25f;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = character.GetComponent<Character>().maxHealth;
        ultBar.maxValue = character.GetComponent<Character>().maxUlt;
        //setting character to the first character on startup
        if (character == null && CharacterList.Count >= 1) {
            character = Instantiate(CharacterList[0], this.transform.position, this.transform.rotation);
            //set the camera to follow the instantiated player
            cameraMovement.PlayerTransform = character.transform;
        }
        // if (CharacterList.Count >= 1) {
        //     for (int i = 0; i < CharacterList.Count; i++) {
        //         if (i != whichCharacter) {
        //            CharacterList[i].SetActive(false); 
        //         }
        //     }
        // }
        //initialize the dictionary of healths, 
        CharacterHealths = new Dictionary<string, float>();
        CharacterUlts = new Dictionary<string, float>();
        foreach (GameObject characterloop in CharacterList){
            CharacterHealths.TryAdd(characterloop.GetComponent<Character>().characterName, characterloop.GetComponent<Character>().maxHealth);
            CharacterUlts.TryAdd(characterloop.GetComponent<Character>().characterName, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatedHealthBar();
        AnimatedUltBar();

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
            Swap(0);
            //PreviousCharacter();
        }

        //swap to 1
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Swap(1);
            //NextCharacter();
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

    public float GetUlt(string charName)
    {
        return CharacterUlts[charName];
    }


    //damage to deal to the player
    public void Damage(float damage){
        
        //CharacterHealths.TryAdd(character.GetComponent<Character>().characterName, 10);
        CharacterHealths[character.GetComponent<Character>().characterName] -= damage;
        //character.GetComponent<Character>().healthBar.SetHealth(CharacterHealths[character.GetComponent<Character>().characterName]);
        time = 0;
        //Debug.Log("Enemy did damage!");
        if (CharacterHealths[character.GetComponent<Character>().characterName] < 0){
            Debug.Log("Character " + character.GetComponent<Character>().characterName + " died!");
            //character died / incapacitated
            //swap to another character that isn't dead?
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

    private void AnimatedHealthBar()
    {
        float targetHealth = CharacterHealths[character.GetComponent<Character>().characterName];
        float startHealth = healthBar.value;
        time += Time.deltaTime * lerpSpeed;
        healthBar.value = Mathf.Lerp(startHealth, targetHealth, time);
    }

    private void AnimatedUltBar()
    {
        float targetHealth = CharacterUlts[character.GetComponent<Character>().characterName];
        float startHealth = ultBar.value;
        time += Time.deltaTime * lerpSpeed;
        ultBar.value = Mathf.Lerp(startHealth, targetHealth, time);
    }
}
