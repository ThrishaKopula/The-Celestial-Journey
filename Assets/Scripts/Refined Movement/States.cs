using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class States 
{
   public Character character;
   public StateMachine stateMachine;

   protected Vector3 gravity;
   protected Vector3 velocity;
   protected Vector3 gravityVelocity;
   protected Vector2 input;

//List of Possible Player Action
   public InputAction moveAction;
   public InputAction dodgeAction;

   public InputAction drawWeaponAction;
   public InputAction lightAttackAction;
   public InputAction heavyAttackAction;

   //Each State Passes the character class and the base state Machine
   public States(Character _character, StateMachine _stateMachine){
        character = _character;
        stateMachine = _stateMachine;
        
        //Initializes all the Player's Actions by pulling from the "Player Controls" Input
        //E.g. MoveAction.triggered will mean that the player is pressing "WASD".

        moveAction = character.playerInput.actions["Player Movement"];
        //dodgeAction = character.playerInput.actions["Dodge"];
        drawWeaponAction = character.playerInput.actions["Draw Weapon"];
        lightAttackAction = character.playerInput.actions["LightAttack"];
        heavyAttackAction = character.playerInput.actions["HeavyAttack"];

   }

   //Enter : Just a Debug statement that tells which state the 
   public virtual void Enter(){
        Debug.Log("Enter State: " + this.ToString());
   }

   //Virtual Functions that can override
   //Logic Updates Handle 
   //Physics Update Handles the Player's physics like 
   //Exit Handles what to do when transitioning out from a state. 
   public virtual void HandleInput(){

   }
   public virtual void LogicUpdate(){
    
   }
   public virtual void PhysicsUpdate(){
    
   }
   public virtual void Exit(){
    
   }
}

