using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TestBoxCollision: MonoBehaviour
{
    BoxCollider weaponHitbox;

    SphereCollider HeavyHitbox;
    CapsuleCollider superHitbox;

    //Find Colliders on Weapons/Players
    //E.g Star Wo
    void Start()
    {
        weaponHitbox = GetComponent<BoxCollider>();
        HeavyHitbox = GetComponent<SphereCollider>();
        superHitbox = GetComponent<CapsuleCollider>();
        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "enemy_hitbox"){
            print("Hit Enemy");
        }
    }

    public void StartDealDamage()
    {
        weaponHitbox.enabled = true;
        print("Makes Hitbox Active");

    }
    public void EndDealDamage()
    {
        weaponHitbox.enabled = false;
        print("DeActivate Hitbox Active");
    }

    public void HeavyDealDamage()
    {
        HeavyHitbox.enabled = true;
        print("Uses a Heavy Attack: Hitbox Active");

    }
    public void HeavyEndDealDamage()
    {
        HeavyHitbox.enabled = false;
        print("Uses a Heavy Attack: Hitbox Deactive");
    }

    public void SuperDealDamage()
    {
        superHitbox.enabled = true;
        print("Uses a Super Attack: Hitbox Active");

    }
    public void SuperEndDealDamage()
    {
        superHitbox.enabled = false;
        print("Uses a Super Attack: Hitbox Deactive");
    }
    
    
}