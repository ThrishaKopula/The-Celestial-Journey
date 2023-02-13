using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RefinedMovement : MonoBehaviour
{
    [Header("Controls")]
    public float playerSpeed = 5.0f;


    [Header("Animation Smoother")]
    [Range(0,1)]
    public float speedDampTime = 0.1f;
    [Range(0,1)]
    public float velocityDampTime = 0.9f;
    [Range(0,1)]
    public float rotationDampTime = 0.2f;

    public StateMachine movement;
    
    private void Start(){
        controller = GetComponent<CharacterController>();   
        anim = GetComponent<Animator>();

    }
}
