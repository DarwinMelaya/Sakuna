using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float sitHeightScale = 0.5f; // Height multiplier when sitting (0.5 = half height)
    
    private Rigidbody rb;
    private Camera playerCamera;
    private float verticalRotation = 0f;
    private bool isSitting = false;
    private Vector3 originalScale;
    private Vector3 originalCameraLocalPosition;
    private bool isGoBagScene; // In Go Bag we keep cursor visible so player can click items

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        
        // Store original scale and camera position
        originalScale = transform.localScale;
        originalCameraLocalPosition = playerCamera.transform.localPosition;
        
        isGoBagScene = SceneManager.GetActiveScene().name == "Go Bag";
        // In Go Bag: gamit ang crosshair sa gitna para mag-aim at mag-collect, hindi ang cursor.
        if (isGoBagScene)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        // Toggle sit with T key
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null && keyboard.tKey.wasPressedThisFrame)
        {
            ToggleSit();
        }
        
        // Mouse look
        Mouse mouse = Mouse.current;
        if (mouse != null)
        {
            Vector2 mouseDelta = mouse.delta.ReadValue();
            float mouseX = mouseDelta.x * mouseSensitivity;
            float mouseY = mouseDelta.y * mouseSensitivity;
            
            // Rotate player horizontally
            transform.Rotate(0, mouseX, 0);
            
            // Rotate camera vertically
            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }

    void FixedUpdate()
    {
        // Movement
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;
        
        float moveX = 0f;
        float moveZ = 0f;
        
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) moveX -= 1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) moveX += 1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) moveZ += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) moveZ -= 1f;
        
        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        movement.Normalize();
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    
    void ToggleSit()
    {
        isSitting = !isSitting;
        
        if (isSitting)
        {
            // Sit down - reduce height
            Vector3 newScale = originalScale;
            newScale.y *= sitHeightScale;
            transform.localScale = newScale;
            
            // Adjust camera position to maintain eye level
            Vector3 newCameraPos = originalCameraLocalPosition;
            newCameraPos.y *= sitHeightScale;
            playerCamera.transform.localPosition = newCameraPos;
        }
        else
        {
            // Stand up - restore original height
            transform.localScale = originalScale;
            playerCamera.transform.localPosition = originalCameraLocalPosition;
        }
    }
}