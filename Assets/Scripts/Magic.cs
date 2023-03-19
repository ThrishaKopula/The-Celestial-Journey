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

    RangedEnemy RGenemy;
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
            GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            gameHandler.CharacterUlts[gameHandler.character.GetComponent<Character>().characterName] += 1;
            enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null){
                Vector3 direction = enemy.transform.position - this.gameObject.transform.position;
                direction.y = 0;
                enemy.GetComponent<Rigidbody>().AddForce(direction.normalized * enemy.movementSpeed * 2 +  new Vector3(0.1f * RGenemy.movementSpeed,1 * RGenemy.movementSpeed * 2,0), ForceMode.Impulse);
                if (enemy.Damage(damage)){
                    Destroy(enemy.gameObject);
                    gameHandler.enemiesDefeated++;
                }
            }
            else{
                
                RGenemy = other.gameObject.GetComponent<RangedEnemy>();
                RGenemy.Damage(damage);
                Vector3 direction = RGenemy.transform.position - this.gameObject.transform.position;
                direction.y = 0;
                RGenemy.GetComponent<Rigidbody>().AddForce(direction.normalized * RGenemy.movementSpeed * 2 +  new Vector3(0.1f * RGenemy.movementSpeed,0,0), ForceMode.Impulse);
                if (RGenemy.Damage(damage)){
                    Destroy(RGenemy.gameObject);
                    gameHandler.enemiesDefeated++;
                }
               

            } 
        }
        Destroy(this.gameObject);
    }
    /*private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "enemy_hitbox"){
            print("pROJECTILE Hit Enemy");
            GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            enemy = other.gameObject.GetComponent<Enemy>();
            if(enemy != null){
                enemy.Damage(damage);
                Vector3 direction = enemy.transform.position - this.gameObject.transform.position;
                direction.y = 0;
                enemy.GetComponent<Rigidbody>().AddForce(direction.normalized * enemy.movementSpeed * 2 +  new Vector3(1 * RGenemy.movementSpeed,1 * RGenemy.movementSpeed * 2,0), ForceMode.Impulse);
                if (enemy.Damage(damage)){
                    Destroy(enemy.gameObject);
                    gameHandler.enemiesDefeated++;
                }
            }
            else{
                RGenemy = other.gameObject.GetComponent<RangedEnemy>();
              
                Vector3 direction = RGenemy.transform.position - this.gameObject.transform.position;
                direction.y = 0;
                RGenemy.GetComponent<Rigidbody>().AddForce(direction.normalized * RGenemy.movementSpeed * 2 +  new Vector3(1 * RGenemy.movementSpeed,1 * RGenemy.movementSpeed * 2,0), ForceMode.Impulse);
                if (RGenemy.Damage(damage)){
                    Destroy(RGenemy.gameObject);
                    gameHandler.enemiesDefeated++;
                }
            }

            }
        //destroy the projectile, it hit something.
        Destroy(this.gameObject);
    }
    */
}
