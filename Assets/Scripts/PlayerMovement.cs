using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] float turnSpeed = 5f;
    public CharacterController controller; // Reference to the Character Controller
    private PlayerInput playerInput; // Reference to the PlayerInput component

    Camera mainCam;
    CameraController cameraController;

    void Start()
    {
        mainCam = Camera.main;
        controller = GetComponent<CharacterController>(); // Assigning the Character Controller reference
        playerInput = GetComponent<PlayerInput>(); // Assigning the PlayerInput component
        cameraController = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        PerformMove();

    }



    private void PerformMove()
    {
        Vector2 moveInput = GetMovementInput();
        Vector2 aimInput = GetAimInput();

        Vector3 moveDirection = CalculateMoveDirection(moveInput);  //moveDirection=(rightDir * moveInput.x + upDir * moveInput.y).normalized;

        Vector3 MoveDir = moveDirection * speed * Time.deltaTime; //to move character face direction

        // Move the character using Character Controller
        controller.Move(MoveDir * speed * Time.deltaTime);

        Vector3 AimDir = MoveDir;

        if (aimInput.magnitude != 0)
        {
            AimDir = StickInputToWorldDir(aimInput);
        }

        RotateTowards(AimDir);
        UpdateCamera(moveInput,aimInput);
    }

    private void UpdateCamera(Vector2 moveInput,Vector2 aimInput)
    {
        //player is moving but not aiming, and cameraController exists
        if (moveInput.magnitude != 0 && aimInput.magnitude==0 && cameraController!=null)  //here we give the value for the x input to change camera rotation
        {
            cameraController.AddYawInput(moveInput.x);
        }
    }

    private void RotateTowards(Vector3 AimDir)
    {
        if (AimDir.magnitude != 0)
        {
            float turnLerpAlpha = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(AimDir, Vector3.up), turnLerpAlpha);
        }
    }


    private Vector2 GetMovementInput()
    {
        // Get movement input from PlayerInput component
        return playerInput.actions["PlayerActionMap/Movement"].ReadValue<Vector2>();
    }

    private Vector2 GetAimInput()
    {
        // Get movement input from PlayerInput component
        return playerInput.actions["PlayerActionMap/Aim"].ReadValue<Vector2>();
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

    private Vector3 StickInputToWorldDir(Vector2 inputVal)
    {
        Vector3 rightDir = mainCam.transform.right;
        Vector3 upDir = Vector3.Cross(rightDir, Vector3.up);
        Vector3 worldDirection = (rightDir * inputVal.x + upDir * inputVal.y).normalized;

        return worldDirection;
    }
}
