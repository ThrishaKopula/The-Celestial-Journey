using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCJ
{
    public class InputManage : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_input;
        public bool rollFlag;
        PlayerControls inputActions;

        Vector2 movementInput;
        Vector2 cameraInput;

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.PlayerMovement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.CameraMovement.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        public void OnDisable(){
            inputActions.Disable();
        }

        public void TickInput(float delta){
            MoveInput(delta);
            HandleRollInput(delta);
        }

        private void MoveInput(float delta){
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleRollInput(float delta){
            b_input = inputActions.PlayerActions.Dodge.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            if(b_input){
                rollFlag = true;
            }
        }
    }


}
