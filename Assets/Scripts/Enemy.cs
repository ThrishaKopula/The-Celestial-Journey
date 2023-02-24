using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;
    public float movementSpeed = 10;

    public float damage = 5;
    Rigidbody rb;

    GameHandler gameHandler;

    private void Awake() {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //move toward player
        Vector3 targetPosition;
        if (gameHandler.character != null){
            targetPosition = gameHandler.character.transform.position;
        }else{
            targetPosition = this.transform.position;
            Debug.Log("Cant find the player!");
        }
        Vector3 direction = targetPosition - this.transform.position;
        direction.y = 0;

        direction = direction.normalized * movementSpeed;

        this.transform.position += direction * Time.deltaTime;
        
    }

    public bool Damage(float damage){
        health -= damage;
        //instead of destroying the enemy gameobject here, i'm returning
        // a boolean for whether or not it has dies. this is so if we want
        // to have a kill count or something else we can do that
        if (health <= 0 ) {
            return true;
        }
        else return false;
    }

    private void OnTriggerEnter(Collider other) {
        Character character = other.GetComponent<Character>();
        if (character != null){
            //do damage
            gameHandler.Damage(damage);
            //do a kickback force to get them out of the collider, but could probably use a delay on another hit
            Vector3 direction = this.transform.position - character.gameObject.transform.position;
            direction.y = 0;
            this.GetComponent<Rigidbody>().AddForce(direction.normalized * movementSpeed * 2 +  new Vector3(0,1 * movementSpeed * 2,0), ForceMode.Impulse);
        }
    }
}
