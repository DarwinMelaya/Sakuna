using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    [Header("Character visual (optional)")]
    [SerializeField] GameObject characterModelPrefab;
    [SerializeField] RuntimeAnimatorController animatorController;
    private CharacterController controller;
    private Animator animator;
    private MeshRenderer defaultMesh;
    private float verticalVelocity = 0f;
    private bool wasMoving = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultMesh = GetComponent<MeshRenderer>();
        if (characterModelPrefab != null)
        {
            GameObject character = Instantiate(characterModelPrefab, transform);
            character.transform.localPosition = Vector3.zero;
            character.transform.localRotation = Quaternion.identity;
            character.transform.localScale = Vector3.one;
            if (defaultMesh != null) defaultMesh.enabled = false;
            animator = character.GetComponentInChildren<Animator>(true);
            if (animator == null) animator = character.AddComponent<Animator>();
            if (animator != null && animatorController != null)
                animator.runtimeAnimatorController = animatorController;
        }
        else
        {
            animator = GetComponentInChildren<Animator>(true);
        }
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        float x = 0f;
        float z = 0f;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed) x -= 1f;
        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed) x += 1f;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed) z += 1f;
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed) z -= 1f;

        Vector3 move = transform.right * x + transform.forward * z;
        float moveMagnitude = new Vector3(x, 0f, z).magnitude;
        move *= speed;
        if (animator != null)
            animator.SetFloat("Speed", moveMagnitude);
        // Handle walking sounds
        bool isMoving = (x != 0f || z != 0f) && controller.isGrounded;
        if (isMoving && !wasMoving)
        {
            // Started moving
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayWalkingSound();
            }
        }
        else if (!isMoving && wasMoving)
        {
            // Stopped moving
            if (AudioManager.instance != null)
            {
                AudioManager.instance.StopWalkingSound();
            }
        }
        wasMoving = isMoving;
        
        // Apply gravity and jumping
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0f)
            {
                verticalVelocity = -2f; // small downward force to keep grounded
            }

            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                // v = √(h * -2 * g)
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}
