using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.0f;
    public float gravity = 9.81f;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left Arrow/Right Arrow
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up Arrow/Down Arrow

        // Calculate movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        moveDirection = move * moveSpeed;

        // Jumping and gravity
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump")) // Default Jump button is Space
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        // Move the player
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
