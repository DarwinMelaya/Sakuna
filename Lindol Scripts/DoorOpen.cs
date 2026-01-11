using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [Header("Door Settings")]
    public Vector3 openPosition; // The position the door should move to when opened
    public float openAngle = 90f; // The rotation angle in degrees (e.g., 90 or 180)
    public float moveSpeed = 2f; // Speed at which the door moves (units per second)
    public float rotationSpeed = 90f; // Rotation speed in degrees per second
    
    private Vector3 closedPosition; // Store the original position
    private Quaternion closedRotation; // Store the original rotation
    private Quaternion openRotation; // The target rotation when opened
    private bool isOpen = false;
    private bool hasOpened = false; // Track if door has been opened at least once
    
    void Awake()
    {
        // Ensure the collider is set as a trigger so player can pass through
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        else
        {
            Debug.LogWarning("DoorOpen: No Collider component found! Please add a Collider component to detect the player.");
        }
    }
    
    void Start()
    {
        // Store the initial position and rotation
        closedPosition = transform.position;
        closedRotation = transform.rotation;
        // Calculate the target rotation by adding the open angle to the Y axis
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }
    
    void Update()
    {
        // Smoothly move and rotate the door to the open position/rotation if it should be open
        if (isOpen && hasOpened)
        {
            // Move the door position
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                moveSpeed * Time.deltaTime
            );
            
            // Rotate the door
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                openRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger and door hasn't opened yet
        if (other.CompareTag("Player") && !hasOpened)
        {
            OpenDoor();
        }
    }
    
    public void OpenDoor()
    {
        if (!hasOpened)
        {
            isOpen = true;
            hasOpened = true;
            Debug.Log("Door opening!");
        }
    }
}
