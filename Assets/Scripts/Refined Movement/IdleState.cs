using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : States
{
   float gravityValue;
   Vector3 currentVelocity;
   float playerSpeed;
   Vector3 cVelocity;
   bool isGrounded;
   bool drawWeapon;

   public IdleState(Character _character, StateMachine _stateMachine) : base(_character,_stateMachine){
        character = _character;
        stateMachine = _stateMachine;
   }

    public override void Enter()
    {
        base.Enter();

        isGrounded = false;
        drawWeapon = false;
        input = Vector2.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0;

        velocity = character.playerVelocity;
        playerSpeed = character.playerSpeed;
        gravityValue = character.gravityValue;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if(drawWeaponAction.triggered){
            drawWeapon = true;
        }

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);

        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        character.animator.SetFloat("Speed",input.magnitude, character.speedDampTime, Time.deltaTime);   

        if(drawWeapon){
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("DrawWeapon");
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
