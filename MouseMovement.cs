using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class MouseMovement : MonoBehaviour
{
 
    public float mouseSensitivity = 100f;
 
    float xRotation = 0f;
    float YRotation = 0f;
 
    void Start()
    {
      //Locking the cursor to the middle of the screen and making it invisible
      Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update()
    {
#if ENABLE_INPUT_SYSTEM
       // New Input System
       Mouse mouse = Mouse.current;
       if (mouse != null)
       {
           Vector2 mouseDelta = mouse.delta.ReadValue();
           float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
           float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;
           
           //control rotation around x axis (Look up and down)
           xRotation -= mouseY;
           
           //we clamp the rotation so we cant Over-rotate (like in real life)
           xRotation = Mathf.Clamp(xRotation, -90f, 90f);
           
           //control rotation around y axis (Look up and down)
           YRotation += mouseX;
           
           //applying both rotations
           transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
       }
#else
       // Old Input System
       float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
       float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
       //control rotation around x axis (Look up and down)
       xRotation -= mouseY;
 
       //we clamp the rotation so we cant Over-rotate (like in real life)
       xRotation = Mathf.Clamp(xRotation, -90f, 90f);
 
       //control rotation around y axis (Look up and down)
       YRotation += mouseX;
 
       //applying both rotations
       transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
#endif
 
    }
}