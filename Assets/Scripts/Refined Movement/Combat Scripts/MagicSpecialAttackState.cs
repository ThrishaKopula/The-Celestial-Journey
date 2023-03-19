using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpecialAttackState : States
{
    float timePassed;
    float clipLength;
    float clipSpeed;
    bool specialAttack;

    Quaternion angle;
    public GameObject projectilePrefab;
    public Vector3 spawnOffset = new Vector3(0,0,0);
    public MagicSpecialAttackState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }
 
    public override void Enter()
    {
        base.Enter();
        specialAttack = false;

        angle = character.transform.rotation;
        GameObject.Instantiate(character.lightMagic, character.transform.position + new Vector3(0, 1, 2), angle);
        GameObject.Instantiate(character.lightMagic, character.transform.position + new Vector3(0, 1, 2), angle * Quaternion.Euler(0, 45, 0));
        GameObject.Instantiate(character.lightMagic, character.transform.position + new Vector3(0, 1, 2), angle * Quaternion.Euler(0, 90, 0));
        GameObject.Instantiate(character.lightMagic, character.transform.position + new Vector3(0, 1, 2), angle * Quaternion.Euler(0, 135, 0));
        GameObject.Instantiate(character.lightMagic, character.transform.position + new Vector3(0, 1, -2), angle * Quaternion.Euler(0, 180, 0));
        GameObject.Instantiate(character.lightMagic, character.transform.position + new Vector3(0, 1, -2), angle * Quaternion.Euler(0, 225, 0));
        GameObject.Instantiate(character.lightMagic, character.transform.position + new Vector3(0, 1, -2), angle * Quaternion.Euler(0, 270, 0));
        GameObject.Instantiate(character.lightMagic, character.transform.position + new Vector3(0, 1, -2), angle * Quaternion.Euler(0, 315, 0));
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("MagicSpecialAttack");
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
            character.animator.ResetTrigger("MagicSpecialAttack");
        }
 
    }
    public override void Exit()
    {
        
        base.Exit();
        character.animator.applyRootMotion = false;
    }
}
