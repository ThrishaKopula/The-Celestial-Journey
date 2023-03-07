using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float MouseSensitivityX = 2f;
    public float MouseSensitivityY = 1.5f;
    public float camMinAngle = -20f;
    public float camMaxAngle = 50f;
    public Transform PlayerTransform;
    public Vector3 cameraOffset = new Vector3(0, 1.84f, -3.18f);
    public GameObject Camera;
    
    float mouseX;
    float mouseY;
    
    private void Start() {
        //Camera.transform.localPosition = cameraOffset;
    }
    void Update()
    {
        //move the camera to its offset behind the character
        Camera.transform.localPosition = cameraOffset;
        //move root of camera to player position
        this.transform.position = PlayerTransform.position;

        //rotate camera parent with mouse
        mouseX += Input.GetAxis("Mouse X") * MouseSensitivityX;
        mouseY += Input.GetAxis("Mouse Y") * MouseSensitivityY;
        mouseY = Mathf.Clamp(mouseY, -camMaxAngle, -camMinAngle);
        transform.localRotation = Quaternion.Euler(-mouseY, mouseX , 0);
        
    }
}
