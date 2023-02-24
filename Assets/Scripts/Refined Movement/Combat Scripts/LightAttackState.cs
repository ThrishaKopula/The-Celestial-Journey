using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class LightAttackState : States
{
    float timePassed;
    float clipLength;
    float clipSpeed;
    bool lightAttack;
    bool heavyAttack;
    public LightAttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
 
    public override void Enter()
    {
        base.Enter();
 
        lightAttack = false;
        heavyAttack = false;
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("LightAttack");
        character.animator.SetFloat("Speed", 0f);
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
 
        if (lightAttackAction.triggered)
        {
            lightAttack = true;
        }
        
        if (heavyAttackAction.triggered)
        {
            heavyAttack = true;
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
 
        timePassed += Time.deltaTime;
        clipLength = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed;
 
        if (timePassed <= clipLength / clipSpeed && lightAttack)
        {
            stateMachine.ChangeState(character.lightAttacking);
        }
        if (timePassed <= clipLength / clipSpeed && heavyAttack)
        {
            stateMachine.ChangeState(character.heavyAttacking);
        }
        if (timePassed >= clipLength / clipSpeed)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("Move");
        }
 
    }
    public override void Exit()
    {
        
        base.Exit();
        character.animator.applyRootMotion = false;
    }
}