using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState: States
{
    float timePassed;
    float clipLength;
    float clipSpeed;
    Vector3 airVelocity;
 
    public DeadState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
 
    public override void Enter()
    {
        base.Enter();
        

    }
    public override void HandleInput()
    {
        base.HandleInput();
 
        input = moveAction.ReadValue<Vector2>();
        input = new Vector2(0,0);
    }
 
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        clipLength = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed;
        
        timePassed += Time.deltaTime;
        if (timePassed >= clipLength * 1.5 / clipSpeed)
        {
            GameHandler gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
            gameHandler.ForceSwap();
            
        }
 
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        velocity = new Vector3(0,0,0);
    }
    public override void Exit()
    {
        base.Exit();
        character.animator.applyRootMotion = false;
    }

}
