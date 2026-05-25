using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Collections;

public class PetAnimal : MonoBehaviour, IInteractable
{
    private Animator animator;
    private NavMeshAgent agent;
    private Transform player;

    public float wanderRadius;
    public float wanderWaitTime;
    public float followTimeAfterPet;

    private bool isPlayerClose = false;
    private bool isBeingPetted = false;
    private bool isFollowingPlayer = false;
    private float aiTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        SetNewWanderDestination();
    }

    public bool Interact()
    {
        StartCoroutine(PetRoutine());
        return false;    
    }

    public void Dismiss()
    {
        return;
    }

    void Update()
    {
        bool isWalking = agent.velocity.magnitude > 0.1f;
        animator.SetBool("IsWalking", isWalking);

        if (isBeingPetted) 
        {
            return;
        }

        if (isFollowingPlayer)
        {
            agent.SetDestination(player.position);
            return; 
        }

        if (isPlayerClose)
        {
            agent.ResetPath(); //stop
            
            Vector3 lookDirection = player.position - transform.position;
            lookDirection.y = 0;
            if (lookDirection != Vector3.zero) // look at player
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 5f);
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            aiTimer += Time.deltaTime; // ai timer starts until reaches the set wait time 
            if (aiTimer >= wanderWaitTime)
            {
                SetNewWanderDestination(); // do wander
                aiTimer = 0f; // reset timer
            }
        }
    }

    private IEnumerator PetRoutine()
    {
        isBeingPetted = true;
        isFollowingPlayer = false;
        agent.ResetPath(); 

        animator.SetBool("IsHappy", true);
        Debug.Log("You pet the animal!");
        animator.SetTrigger("PetTrigger");
        
        yield return new WaitForSeconds(2f); // wait for jump to finish (about 2sec?)

        isBeingPetted = false;
        StartCoroutine(FollowRoutine());
    }

    private IEnumerator FollowRoutine()
    {
        Debug.Log("Cat is following you");
        isFollowingPlayer = true;
        
        yield return new WaitForSeconds(followTimeAfterPet);
        
        Debug.Log("Cat no longer follows you");
        isFollowingPlayer = false;
        animator.SetBool("IsHappy", false);
        SetNewWanderDestination();
    }

    private void SetNewWanderDestination() // TODO: maybe add some direction probabilioty later
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1)) // NavMesh.SamplePosition - destination on NavMesh so can be reached by cat
        {
            agent.SetDestination(hit.position);
        }
    }

    //built in unity functions for trigger colliders
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
            Debug.Log("Player out of range of cat.");
        }
    }
}