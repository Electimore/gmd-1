using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PetAnimal : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerClose = false;
    private bool isPetting = false;
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
        if (isPlayerClose && !isPetting && interactAction.triggered)
        {
            Pet();
        }
    }

    private void Pet()
    {
        Debug.Log("You pet the cat!");

        if (animator != null)
        {
            animator.SetTrigger("PetTrigger");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = true;
            Debug.Log("Player in range of cat.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
            Debug.Log("Player no longer in range of cat.");
        }
    }
}
