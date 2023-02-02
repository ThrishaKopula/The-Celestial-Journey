using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //souce for camera movement that i modified: https://gist.github.com/gunderson/d7f096bd07874f31671306318019d996
    public float MouseSensitivityX = 2f;
    public float MouseSensitivityY = 1.5f;
    public float camMinAngle = -20f;
    public float camMaxAngle = 50f;
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    
    private void Start() {

    }
    // Update is called once per frame
    float mouseX;
    float mouseY;
    
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * MouseSensitivityX;
        mouseY += Input.GetAxis("Mouse Y") * MouseSensitivityY;
        mouseY = Mathf.Clamp(mouseY, -camMaxAngle, -camMinAngle);
        transform.localRotation = Quaternion.Euler(-mouseY, mouseX , 0);
        
    }
}
