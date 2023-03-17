using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Refined Movement: Contains all information for the player so that we can access 
public class Character : MonoBehaviour
{

    public string characterName = "defaultName";
    public float maxHealth = 10;
    public float maxUlt = 10;

    [Header("Controls")]
    public float playerSpeed = 5.0f;
    public float gravityMultiplier = 2;
    public float rotationSpeed = 5f;

    [Header("Animation Smoother")]
    [Range(0,1)]
    public float speedDampTime = 0.1f;
    [Range(0,1)]
    public float velocityDampTime = 0.9f;
    [Range(0,1)]
    public float rotationDampTime = 0.2f;
    [Range(0,1)]
    public float airControl = 0.5f;

    public StateMachine movementSM;
    public IdleState idle;
    public CombatState combatting;

    public LightAttackState lightAttacking;

    public HeavyAttackState heavyAttacking;

    public SpecialAttackState specialAttacking;

    public DodgeState dodge;

    //Gravity for the character
    [HideInInspector]
    public float gravityValue = -9.81f;

    //Default Character's Collider Height
    [HideInInspector]
    public float normalColliderHeight;

    //Character Input Controller
    [HideInInspector]
    public CharacterController controller;

    [HideInInspector]
    public PlayerInput playerInput;

    //Character Animator
    [HideInInspector]
    public Animator animator;

    //Character Animator
    [HideInInspector]
    public Vector3 playerVelocity;

    //Character Camera
    [HideInInspector]
    public Transform cameraTransform;
    
    private void Start(){
        //Initialize the following:
        // - Controller : The Player CharacterController Object
        // - Animator: The Player Animator
        // - PlayerInput: The Player's InputSystem 
        // - Cameratransform: The Camera's position. 
        controller = GetComponent<CharacterController>();   
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
       
        //Instantiate New States for the Character
        //Idle : Base Movement
        //Combat : Combat Movement
        //Attacking: While the player is attacking
        movementSM = new StateMachine();
        idle = new IdleState(this,movementSM);
        combatting = new CombatState(this,movementSM);
        lightAttacking = new LightAttackState(this,movementSM);
        heavyAttacking = new HeavyAttackState(this,movementSM);
        specialAttacking = new SpecialAttackState(this,movementSM);
        dodge = new DodgeState(this,movementSM);
        
        movementSM.Initialize(combatting);

        //Normal Character Collider Height. From the info from Character Controller Object.
        normalColliderHeight = controller.height;

    }

    //Update Logic and Update by overriding the functions
    private void Update(){
        movementSM.currentState.HandleInput();
        movementSM.currentState.LogicUpdate();
    }

    //Fixed Update updates the Physics for the player. 
    private void FixedUpdate(){
        movementSM.currentState.PhysicsUpdate();
    }
}

