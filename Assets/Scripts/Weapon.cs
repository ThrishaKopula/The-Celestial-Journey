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
        if (enemy != null){
            if (enemy.Damage(damage)){
                Destroy(enemy.gameObject);
            }
        }
    }
}
