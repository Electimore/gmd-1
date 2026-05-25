using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Transform player;
    public float triggerDistance;
    public float slideDistance;
    public float slideSpeed;
    public AudioClip doorSound;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private bool wasOpen = false;
    private AudioSource audioSource;

    void Start()
    {
        closedPosition = transform.position;
        // slide direction
        openPosition = transform.position + transform.forward * slideDistance;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector3.Distance(closedPosition, player.position);

        isOpen = distance <= triggerDistance;

        if (isOpen != wasOpen)
        {
            audioSource.PlayOneShot(doorSound);
            wasOpen = isOpen;
        }

        Vector3 target = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.MoveTowards(transform.position, target, slideSpeed * Time.deltaTime);
    }
}