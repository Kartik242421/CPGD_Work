using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public CharacterController controller; // Reference to the Character Controller
    private PlayerInput playerInput; // Reference to the PlayerInput component

    Camera mainCam;
    CameraController cameraController;

    [System.Obsolete]
    void Start()
    {
        mainCam = Camera.main;
        controller = GetComponent<CharacterController>(); // Assigning the Character Controller reference
        playerInput = GetComponent<PlayerInput>(); // Assigning the PlayerInput component
        cameraController = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        Vector2 moveInput = GetMovementInput();
        Vector3 moveDirection = CalculateMoveDirection(moveInput);

        // Move the character using Character Controller
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (moveInput.magnitude != 0 && cameraController != null)  //here we give the value for the x input to change camera rotation
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private Vector2 GetMovementInput()
    {
        // Get movement input from PlayerInput component
        return playerInput.actions["PlayerActionMap/Movement"].ReadValue<Vector2>();
    }

    private Vector3 CalculateMoveDirection(Vector2 moveInput)
    {
        // Calculate movement based on camera's orientation
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        Vector3 moveDirection = (rightDir * moveInput.x + upDir * moveInput.y).normalized;

        // Keep the y-component constant
        moveDirection.y = 0f;

        return moveDirection;
    }
}
