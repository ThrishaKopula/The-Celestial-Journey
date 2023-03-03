using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MagicLightAttackState : States
{
    float timePassed;
    float clipLength;
    float clipSpeed;
    bool magicLightAttack;
    bool magicHeavyAttack;
    public GameObject projectilePrefab;
    public Vector3 spawnOffset = new Vector3(0,0,0);
    public MagicLightAttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
 
    public override void Enter()
    {
        base.Enter();
 
        magicLightAttack = false;
        magicHeavyAttack = false;
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("MagicLightAttack");
        character.animator.SetFloat("Speed", 0f);

    }
 
    public override void HandleInput()
    {
        base.HandleInput();
 
        if (lightAttackAction.triggered)
        {
            magicLightAttack = true;
        }
        
        if (heavyAttackAction.triggered)
        {
            magicHeavyAttack = true;
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timePassed += Time.deltaTime;
        clipLength = character.animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
        clipSpeed = character.animator.GetCurrentAnimatorStateInfo(1).speed;

        if (timePassed >= (clipLength / clipSpeed)/2){//if half-way through the animation:
            //spawn projectile:
            GameObject.Instantiate(projectilePrefab, character.transform.position, character.transform.rotation, character.transform);
            //this assumes that the projectile has it's own homing, which magic probably will. 
        }
 
        if (timePassed <= clipLength / clipSpeed && magicLightAttack)
        {
            //stateMachine.ChangeState(character.magicLightAttacking);
        }
        if (timePassed <= clipLength / clipSpeed && magicHeavyAttack)
        {
            //stateMachine.ChangeState(character.magicHeavyAttacking);
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