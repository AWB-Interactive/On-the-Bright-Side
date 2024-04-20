using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 lookInput;
    public float lookSensitivity = 100f;
    public float verticalLookLimit = 80f; // Adjust this value to set the vertical look limit
    private float accumulatedVerticalRotation = 0f; // Track total accumulated vertical rotation

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        controls = new PlayerControls();
        controls.Enable();

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void Update()
    {
        // Rotate the camera based on mouse movement or gamepad input
        float mouseX = lookInput.x * lookSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * lookSensitivity * Time.deltaTime;

        // Rotate around the y-axis (horizontal rotation)
        transform.Rotate(Vector3.up * mouseX);

        // Vertical rotation (up and down)
        float desiredRotationX = -mouseY; // Invert mouseY for natural vertical rotation

        // Accumulate the vertical rotation
        accumulatedVerticalRotation += desiredRotationX;

        // Clamp the accumulated vertical rotation within the specified range
        accumulatedVerticalRotation = Mathf.Clamp(accumulatedVerticalRotation, -verticalLookLimit, verticalLookLimit);

        // Apply the rotation
        transform.localRotation = Quaternion.Euler(accumulatedVerticalRotation, transform.localRotation.eulerAngles.y, 0f);
    }
}