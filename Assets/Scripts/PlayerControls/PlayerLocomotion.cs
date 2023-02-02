using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCJ{
public class PlayerLocomotion : MonoBehaviour
{
    Transform cameraObject;
    InputManage inputHandler;
    Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;

    [HideInInspector]
    public AnimatorManager animatorManager;
    public new Rigidbody rigidbody;
    public GameObject normalCamera;
    // Start is called before the first frame update
    [Header("Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputManage>();
        animatorManager = GetComponentInChildren<AnimatorManager>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animatorManager.Initialize();
    }

    public void Update(){
        float delta = Time.deltaTime;

        inputHandler.TickInput(delta);
        HandleMovement(delta);
        HandleRollingSprinting(delta);
        
    }

    #region Movement
    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta){
        Vector3 targetDir = Vector3.zero;
        float moveOvverride = inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero){
            targetDir = myTransform.forward;
        }

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr,rs*delta);

        myTransform.rotation = targetRotation;
    }
    #endregion

    private void HandleMovement(float delta){
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();

        float speed = movementSpeed;
        moveDirection *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;

        animatorManager.UpdateAnimator(inputHandler.moveAmount,0);

        if(animatorManager.camRotate){
            HandleRotation(delta);
        }
    }

    private void HandleRollingSprinting(float delta){
            if(animatorManager.anim.GetBool("isInteracting")){
                return;
            }
            if(inputHandler.rollFlag){
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if(inputHandler.moveAmount > 0){
                    animatorManager.PlayTargetAnimation("Rolling", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                }
                else{
                    animatorManager.PlayTargetAnimation("BackStep",true);
                }
            }
            

    }
}

}
