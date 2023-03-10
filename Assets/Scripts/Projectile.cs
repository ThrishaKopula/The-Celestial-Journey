using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 10.0f;
    public float speed = 6.0f;
    public float damage = 3.0f;
    

    private float instantiatedTime;
    GameHandler gameHandler;
    Rigidbody rb;

    private void Awake() {
        instantiatedTime = Time.time;
        gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        rb = this.GetComponent<Rigidbody>();
        if (gameHandler.character != null){
            rb.velocity = (gameHandler.character.transform.position - this.transform.position + new Vector3(0,1,0)).normalized * speed;
        }
    }

    private void Update() {
        //check if lifetime has run out:
        if (Time.time >= instantiatedTime + lifetime) {
            //object has been alive too long!
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Character character = other.GetComponent<Character>();
        if (character != null){
            //do damage to player
            gameHandler.Damage(damage);
            //destroy the projectile since it hit the player
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other) {
        Character character = other.gameObject.GetComponent<Character>();
        if (character != null){
            //do damage to player
            gameHandler.Damage(damage);
            
        }
        //destroy the projectile, it hit something.
        Destroy(this.gameObject);
    }
}
