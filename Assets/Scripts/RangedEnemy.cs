using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public float health = 10.0f;
    public float movementSpeed = 2.0f;
    public float minimumDistance = 10.0f;
    public float maximumDistance = 30.0f;
    public float timeBetweenAttacks = 3.0f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPosition;
    

    Rigidbody rb;

    GameHandler gameHandler;
    private string state = "Waiting";
    private float waitTimeInitiated = 0;

    private void Awake() {
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition;
        Vector3 direction;
        Vector3 lookAtPosition;
        //basically using a state machine for the enemy.
        switch (state) {
            case "Fleeing":
                if (gameHandler.character != null){
                    targetPosition = gameHandler.character.transform.position;
                }else{
                    targetPosition = this.transform.position;
                    Debug.Log("Cant find the player!");
                }
                direction = this.transform.position - targetPosition;
                //look away from player
                lookAtPosition = (this.transform.position + direction);
                lookAtPosition.y = this.transform.position.y;
                this.transform.LookAt(lookAtPosition, Vector3.up);

                // switch state if it gets far enough away.
                if (direction.magnitude >= minimumDistance) state = "Attack"; 
                direction.y = 0;//dont move the characters vertically
                direction = direction.normalized * movementSpeed;
                this.transform.position += direction * Time.deltaTime;
                break;
            
            case "Approaching":
                if (gameHandler.character != null){
                    targetPosition = gameHandler.character.transform.position;
                }else{
                    targetPosition = this.transform.position;
                    Debug.Log("Cant find the player!");
                }
                
                direction = targetPosition - this.transform.position;
                //look at player
                lookAtPosition = (this.transform.position + direction);
                lookAtPosition.y = this.transform.position.y;
                if (this.transform.position != targetPosition){
                    this.transform.LookAt(lookAtPosition, Vector3.up);// wont try to look at itself, cause that might cause problems
                }
                // switch state if it gets close enough
                if (direction.magnitude <= maximumDistance) state = "Attack"; 
                direction.y = 0;//dont move the characters vertically
                direction = direction.normalized * movementSpeed;
                this.transform.position += direction * Time.deltaTime;
                break;

            case "Attack":
                //Attack the player
                //look at the player:
                if (gameHandler.character != null){
                    targetPosition = gameHandler.character.transform.position;
                }else{
                    targetPosition = this.transform.position;
                    Debug.Log("Cant find the player!");
                }
                direction = targetPosition - this.transform.position;
                //look at player
                lookAtPosition = (this.transform.position + direction);
                lookAtPosition.y = this.transform.position.y;
                this.transform.LookAt(lookAtPosition, Vector3.up);

                //start an animation here? <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                //Spawn the prefab of the projectile
                Vector3 position;
                if (projectileSpawnPosition != null)
                    {position = projectileSpawnPosition.position;}
                else {position = this.transform.position;}

                if (projectilePrefab != null){
                    GameObject projectile = Instantiate(projectilePrefab, position, this.transform.rotation);
                    //either add the projectile velocity here, or let the projectile do this on it's own.
                }

                //switch to wait for attack delay
                state = "Waiting";
                waitTimeInitiated = Time.time;

                break;

            case "Waiting":
                //wait for "timeBetweenAttacks" seconds. This could be done with a coroutine, but
                // screw it i'm doing it like this.
                if (gameHandler.character != null){
                    targetPosition = gameHandler.character.transform.position;
                }else{
                    targetPosition = this.transform.position;
                    Debug.Log("Cant find the player!");
                }
                direction = targetPosition - this.transform.position;
                //keep looking at player
                lookAtPosition = (this.transform.position + direction);
                lookAtPosition.y = this.transform.position.y;
                this.transform.LookAt(lookAtPosition, Vector3.up);
                
                if (Time.time >= waitTimeInitiated + timeBetweenAttacks) {
                    //get the vector from this enemy to the character
                    
                    float distance = direction.magnitude;
                    if (distance > maximumDistance){
                        state = "Approaching";
                    }else if (distance < minimumDistance){
                        state = "Fleeing";
                    }else{
                        state = "Attack";
                    }
                }
                break;
            default:
                break;
        }
        
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

}
