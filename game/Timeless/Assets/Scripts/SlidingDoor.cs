using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform player;
    public float triggerDistance;
    public float slideDistance;
    public float slideSpeed;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.position;
        // slide direction
        openPosition = transform.position + transform.forward * slideDistance;
    }

    void Update()
    {
        float distance = Vector3.Distance(closedPosition, player.position);

        isOpen = distance <= triggerDistance;

        Vector3 target = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.MoveTowards(transform.position, target, slideSpeed * Time.deltaTime);
    }
}