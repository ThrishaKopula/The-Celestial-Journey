using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class MagicLightAttackState : States
{
    float timePassed;
    float clipLength;
    float clipSpeed;
    Quaternion angle;
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
        angle = character.transform.rotation;
        GameObject.Instantiate(character.lightMagic, character.transform.position + new Vector3(0, 1, 1), angle);
        magicLightAttack = false;
        magicHeavyAttack = false;
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("ProjectileAttack");
        character.animator.SetFloat("Speed", 0f);

    }
 
    public override void HandleInput()
    {
        base.HandleInput();
 
        if (magicLightAttackAction.triggered)
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
        if (timePassed <= clipLength / clipSpeed && magicLightAttack)
        {
            stateMachine.ChangeState(character.magicLightAttacking);
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