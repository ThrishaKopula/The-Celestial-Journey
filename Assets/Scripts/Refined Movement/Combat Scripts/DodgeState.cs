using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState: States
{
    bool grounded;
    float playerSpeed;
 
    float timePassed;
    float clipLength;
    float clipSpeed;
    Vector3 airVelocity;
 
    public DodgeState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
 
    public override void Enter()
    {
        base.Enter();
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = 0;
 
        character.animator.SetTrigger("Dodge");

    }
    public override void HandleInput()
    {
        base.HandleInput();
 
        input = moveAction.ReadValue<Vector2>();
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        clipLength = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed;
        
        timePassed += Time.deltaTime;
        if (timePassed >= clipLength / clipSpeed)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("Move");
            
        }
 
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        velocity = character.playerVelocity;
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        character.controller.Move(velocity * 30f * Time.deltaTime);
    }
    public override void Exit()
    {
        base.Exit();
        character.animator.applyRootMotion = false;
    }

}
