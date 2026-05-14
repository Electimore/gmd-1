using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonPlayer : MonoBehaviour
{
    public float xSensitivity, ySensitivity;
    public float speed;
    public float jumpForce;
    bool isGrounded;
    public Transform cameraTransform;
    Vector3 movementDirection;
    Rigidbody rb;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb=GetComponent<Rigidbody>();
        isGrounded = true;
    }

    public void OnMouseX(InputAction.CallbackContext context)
    {
        float deltaX = context.ReadValue<float>() * xSensitivity;
        transform.Rotate(0f, deltaX, 0f);
    }

    public void OnMouseY(InputAction.CallbackContext context){
        float deltaY = context.ReadValue<float>() * ySensitivity;
        Vector3 newRotation = cameraTransform.rotation.eulerAngles + new Vector3(deltaY, 0f, 0f);
        cameraTransform.rotation = Quaternion.Euler(newRotation);
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

    void Update()
    {
        rb.MovePosition(transform.position + transform.TransformDirection(movementDirection) * speed * Time.deltaTime);
    }
}
