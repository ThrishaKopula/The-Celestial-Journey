using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName = "TestName"; // Bloodied Cleaver of Doom idk
    public string type = "sword"; //polearm, staff, ect

    public float damage = 10; // we could add damage typess and attribute stuff if we want

    private void OnTriggerEnter(Collider other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        RangedEnemy rangedEnemy = other.gameObject.GetComponent<RangedEnemy>();
        if (enemy != null){
            GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            gameHandler.CharacterUlts[gameHandler.character.GetComponent<Character>().characterName] += 1;
            Debug.Log(gameHandler.CharacterUlts[gameHandler.character.GetComponent<Character>().characterName]);
            if (enemy.Damage(damage)){
                Destroy(enemy.gameObject);
            }
        }
        else if (rangedEnemy != null){
            GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            gameHandler.CharacterUlts[gameHandler.character.GetComponent<Character>().characterName] += 1;
            Debug.Log(gameHandler.CharacterUlts[gameHandler.character.GetComponent<Character>().characterName]);
            if (rangedEnemy.Damage(damage)){
                Destroy(rangedEnemy.gameObject);
            }
        }
        
    }
}
