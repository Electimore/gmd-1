using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float sphereRadius = 0.5f;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;

    // Track state
    private IInteractable currentInteractable = null;
    private bool isPausedForInteraction = false;

    // Hover & Outline tracking
    private GameObject currentlyHoveredObject = null;
    private Outline currentOutline = null;

    private void OnEnable()
    {
        interactAction.action.performed += OnInteractPressed;
        interactAction.action.Enable();
    }

    private void OnDisable()
    {
        interactAction.action.performed -= OnInteractPressed;
        interactAction.action.Disable();
    }

private void Update()
    {
        if (isPausedForInteraction) return;

        if (Physics.SphereCast(cameraTransform.position, sphereRadius, cameraTransform.forward, out RaycastHit hit, interactRange, interactableLayer, QueryTriggerInteraction.Ignore))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (hitObject != currentlyHoveredObject)
                {
                    ClearHighlight();       
                    HighlightObject(hitObject); 
                }
            }
            else
            {
                ClearHighlight();
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    private void OnInteractPressed(InputAction.CallbackContext context)
    {
        if (isPausedForInteraction && currentInteractable != null)
        {
            currentInteractable.Dismiss();
            Time.timeScale = 1f;
            isPausedForInteraction = false;
            currentInteractable = null;
            return;
        }

        if (currentlyHoveredObject != null)
        {
            IInteractable interactable = currentlyHoveredObject.GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                bool requiresDismissal = interactable.Interact();

                if (requiresDismissal)
                {
                    currentInteractable = interactable;
                    isPausedForInteraction = true;
                    Time.timeScale = 0f;
                    
                    ClearHighlight(); 
                }
            }
        }
    }

    private void HighlightObject(GameObject obj)
    {
        currentlyHoveredObject = obj;

        currentOutline = obj.GetComponent<Outline>();
        
        if (currentOutline == null)
        {
            currentOutline = obj.AddComponent<Outline>();
            currentOutline.OutlineMode = Outline.Mode.OutlineAll;
            currentOutline.OutlineColor = Color.yellow;
            currentOutline.OutlineWidth = 5f;
        }
        
        currentOutline.enabled = true;
    }

    private void ClearHighlight()
    {
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }
        
        currentlyHoveredObject = null;
    }
}