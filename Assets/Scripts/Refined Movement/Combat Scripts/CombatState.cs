using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatState : States
{
   float gravityValue;
   Vector3 currentVelocity;
   float playerSpeed;
   Vector3 cVelocity;
   bool isGrounded;
   bool isDashing;
   bool sheathWeapon;
   bool lightAttackCombo;

   bool magicLightAttackCombo;

   bool dodging;
   bool heavyAttack;

   bool specialAttack;

   bool magicSpecialAttack;

  
   bool isMagic;

   bool isDead;
  

   public CombatState(Character _character, StateMachine _stateMachine) : base(_character,_stateMachine){
        character = _character;
        stateMachine = _stateMachine;
   }

    public override void Enter()
    {
        base.Enter();

        sheathWeapon = false;
        isGrounded = false;
        lightAttackCombo = false;
        magicLightAttackCombo = false;
        heavyAttack = false;
        specialAttack = false;
        magicSpecialAttack = false;
        isDashing = false;
       
        dodging = false;
        
        input = Vector2.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        gravityValue = character.gravityValue;
        isMagic = character.isMagic;
        isDead = character.isDead;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        

        if(drawWeaponAction.triggered){
            sheathWeapon = true;
        }


        //Enter Dash State 
        if(dodgeAction.triggered){
            dodging = true;
        }


        //If Light Attack Action triggered Set LightAttack to True;
        if(lightAttackAction.triggered){
            lightAttackCombo = true;
        }

        //If Heavy Attack Action triggered Set LightAttack to True;
        if(heavyAttackAction.triggered){
            heavyAttack = true;
        }

        if(magicLightAttackAction.triggered){
            magicLightAttackCombo = true;
        }

        
        if(magicSpecialAttackAction.triggered){
            magicSpecialAttack = true;
        }

        //If Heavy Attack Action triggered Set LightAttack to True;
        if(specialAttackAction.triggered){
            GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            if (gameHandler.CharacterUlts[gameHandler.character.GetComponent<Character>().characterName] >= character.maxUlt)
            {
                specialAttack = true;
                
            }
            
        }

        //Read the Player Input and Create a new vector 2 in the direction of the inputs.
        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isDead = character.isDead;
        character.animator.SetFloat("Speed",input.magnitude, character.speedDampTime, Time.deltaTime);   
        GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        
        if(isDead){
            character.animator.SetTrigger("isDead");
            stateMachine.ChangeState(character.dead);
        }
        if(dodging){
            character.animator.SetTrigger("Dodge");
            stateMachine.ChangeState(character.dodge);
        }

        if(sheathWeapon){
            character.animator.SetTrigger("SheathWeapon");
            stateMachine.ChangeState(character.idle);
        } 

        if(lightAttackCombo && !isMagic){
            character.animator.SetTrigger("LightAttack");
            stateMachine.ChangeState(character.lightAttacking);
        } 
        
        if(magicLightAttackCombo && isMagic){
            character.animator.SetTrigger("ProjectileAttack");
            stateMachine.ChangeState(character.magicLightAttacking);
        } 

        if(heavyAttack){
            character.animator.SetTrigger("HeavyAttack");
            stateMachine.ChangeState(character.heavyAttacking);
        } 

         if(specialAttack && !isMagic){
            character.animator.SetTrigger("SpecialAttack");
            stateMachine.ChangeState(character.specialAttacking);
        } 

        if(specialAttack && isMagic){
            character.animator.SetTrigger("MagicSpecialAttack");
            stateMachine.ChangeState(character.magicSpecialAttacking);
        } 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
 
        gravityVelocity.y += gravityValue * Time.deltaTime;
       
        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }



        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity,ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);
  
        if (velocity.sqrMagnitude>0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity),character.rotationDampTime);
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        gravityVelocity.y = 0f;
        character.playerVelocity = new Vector3(input.x,0,input.y);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }

    }
}
