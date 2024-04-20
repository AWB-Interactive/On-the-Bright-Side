using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;
    private Vector3 lookDirection;
    public float moveSpeed = 5f;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        // Subscribe to both keyboard and gamepad movement input
        controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    // void Update()
    // {
    //     // // Get the movement input
    //     // Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

    //     // // Calculate the look direction based on the camera's forward vector
    //     // lookDirection = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

    //     // // Rotate the movement direction to match the player's orientation
    //     // moveDirection = Quaternion.LookRotation(lookDirection) * moveDirection;

    //     // // Move the player in the direction they are facing
    //     // transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    // }

    void Update()
    {
    // Get the movement input
    Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

    // Calculate the look direction based on the camera's forward vector
    lookDirection = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

    // Rotate the movement direction to match the player's orientation
    moveDirection = Quaternion.LookRotation(lookDirection) * moveDirection;

    // Scale the moveDirection based on the magnitude of the input for smoother movement
    float inputMagnitude = moveInput.magnitude;
    moveDirection *= Mathf.Lerp(0.5f, 1f, inputMagnitude); // Adjust the range as needed

    // Move the player in the direction they are facing
    transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
}
}