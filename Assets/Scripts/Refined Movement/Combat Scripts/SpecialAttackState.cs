using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackState : States
{
    float timePassed;
    float clipLength;
    float clipSpeed;
    bool specialAttack;
    public SpecialAttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
 
    public override void Enter()
    {
        base.Enter();
        specialAttack = false;
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("SpecialAttack");
        character.animator.SetFloat("Speed", 0f);
    }
 
    public override void HandleInput()
    {
        base.HandleInput();
 
        if (specialAttackAction.triggered)
        {
            specialAttack = true;
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
 
        timePassed += Time.deltaTime;
        clipLength = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed;
 
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
