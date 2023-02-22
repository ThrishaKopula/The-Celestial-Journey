using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10;
    public float movementSpeed = 10;
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

    public bool damage(float damage){
        health -= damage;
        if (health <= 0 ) {
            return true;
        }
        else return false;
    }
}
