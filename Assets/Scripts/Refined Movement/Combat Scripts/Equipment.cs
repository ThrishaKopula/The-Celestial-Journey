using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
//Equipment Script: Pass in the Weapon Prefab, WeaponHolder Position (Where the Player holds the weapon) and SheathHolder Position (Where the player sheaths the weapon).
public class Equipment : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;
 
    
    GameObject currentWeaponInHand;
    GameObject currentWeaponInSheath;

    //Since the Player Starts in an idle state, you place the weapon in their sheath
    void Start()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
    }
 
    //Draw the weapon from Sheath: You destroy the weapon object in the sheath and move the weapon to the character's hand.
    public void DrawWeapon()
    {
        currentWeaponInHand = Instantiate(weapon, weaponHolder.transform);
        Destroy(currentWeaponInSheath);
    }
 
    //Vice Versa. Destroy Weapon in hand and put weapon in Sheath.
    public void SheathWeapon()
    {
        currentWeaponInSheath = Instantiate(weapon, weaponSheath.transform);
        Destroy(currentWeaponInHand);
    }
    
    public void StartDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<TestBoxCollision>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<TestBoxCollision>().EndDealDamage();
    }

    public void HeavyDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<TestBoxCollision>().HeavyDealDamage();
    }
    public void HeavyEndDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<TestBoxCollision>().HeavyEndDealDamage();
    }

    public void SuperDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<TestBoxCollision>().SuperDealDamage();
    }
    public void SuperEndDealDamage()
    {
        currentWeaponInHand.GetComponentInChildren<TestBoxCollision>().SuperEndDealDamage();
    }

}