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

   public States(Character _character, StateMachine _stateMachine){
        character = _character;
        stateMachine = _stateMachine;
        
        moveAction = character.playerInput.actions["PlayerMovement"];
        dodgeAction = character.playerInput.actions["Dodge"];

   }

   public virtual void Enter(){
        Debug.Log("Enter State: " + this.ToString());
   }

   //Virtual Functions that can override
   public virtual void HandleInput(){

   }
   public virtual void LogicUpdate(){
    
   }
   public virtual void PhysicsUpdate(){
    
   }
   public virtual void Exit(){
    
   }
}

