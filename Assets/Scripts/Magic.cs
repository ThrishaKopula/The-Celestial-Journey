using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic: MonoBehaviour
{
    public float lifetime = 10.0f;
    public float speed = 6.0f;
    public float damage = 3.0f;
    

    private float instantiatedTime;
    Enemy enemy;
    Rigidbody rb;

    private void Awake() {
        instantiatedTime = Time.time;
    }

    private void Update() {
        //check if lifetime has run out:
        Vector3 magicVel = Vector3.forward * speed;
        transform.Translate(magicVel * Time.deltaTime);
        if (Time.time >= instantiatedTime + lifetime) {
            //object has been alive too long!
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "enemy_hitbox"){
            print("pROJECTILE Hit Enemy");
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Damage(damage);   
        }
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "enemy_hitbox"){
            print("pROJECTILE Hit Enemy");
            enemy = other.gameObject.GetComponent<Enemy>();
            enemy.Damage(damage);
            
        }
        //destroy the projectile, it hit something.
        Destroy(this.gameObject);
    }

}
