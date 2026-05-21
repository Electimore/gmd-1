using UnityEngine;
using UnityEngine.InputSystem;


public class PetAnimal : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerClose = false;
    public InputAction interactAction;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        interactAction.Enable();
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }

    void Update()
    {
        if (isPlayerClose && interactAction.triggered)
        {
            Pet();
        }
    }

    private void Pet()
    {
        Debug.Log("You pet the animal!");

        if (animator != null)
        {
            animator.Play("Jump");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = true;
            Debug.Log("Player in range of animal.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
            Debug.Log("Player no longer in range of animal.");
        }
    }
}
