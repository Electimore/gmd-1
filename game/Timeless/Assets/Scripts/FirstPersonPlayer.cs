using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonPlayer : MonoBehaviour
{
    public float xSensitivity, ySensitivity;
    public float speed;
    public float jumpForce;
    public float stickRotationSpeed;
    bool isGrounded;
    public Transform cameraTransform;
    Vector3 movementDirection;
    Vector2 stickLookDircetion;
    Rigidbody rb;

    float mouseXInput;
    float mouseYInput;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        isGrounded = true;

        yRotation = transform.localEulerAngles.y;
        
        float camX = cameraTransform.localEulerAngles.x;
        if (camX > 180f) camX -= 360f;
        xRotation = camX;
    }

    public void OnMouseX(InputAction.CallbackContext context)
    {
        mouseXInput += context.ReadValue<float>();
    }

    public void OnMouseY(InputAction.CallbackContext context)
    {
        mouseYInput += context.ReadValue<float>();
    }

    public void OnStickLook(InputAction.CallbackContext context) 
    {
        stickLookDircetion = context.ReadValue<Vector2>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        movementDirection = new Vector3(input.x, 0f, input.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 targetVelocity = transform.TransformDirection(movementDirection) * speed;
        targetVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = targetVelocity;
    }

    void LateUpdate()
    {
        float totalDeltaX = 0f;
        float totalDeltaY = 0f;

        if (mouseXInput != 0f || mouseYInput != 0f)
        {
            totalDeltaX += mouseXInput * xSensitivity;
            totalDeltaY += mouseYInput * ySensitivity;
        }

        if (stickLookDircetion != Vector2.zero) 
        {
            totalDeltaX += stickLookDircetion.x * xSensitivity * Time.deltaTime * stickRotationSpeed;
            totalDeltaY += stickLookDircetion.y * ySensitivity * Time.deltaTime * stickRotationSpeed;
        }

        if (totalDeltaX != 0f || totalDeltaY != 0f)
        {
            yRotation += totalDeltaX;
            xRotation += totalDeltaY; 

            xRotation = Mathf.Clamp(xRotation, -88f, 60f);

            transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        mouseXInput = 0f;
        mouseYInput = 0f;
    }
}