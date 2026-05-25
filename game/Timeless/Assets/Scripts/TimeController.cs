using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    private int currentTime = 0; // since start of the loop, in seconds
    private int loopDuration = 20; // seconds

    private float timeOffset;
    private float timestampAtPause;

    private List<IInfluencedByTime> influencedObjects;
    private bool transitioning = false;
    private bool loopRunning = true;
    
    [SerializeField]
    Animator transition; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeOffset = 0f;
        influencedObjects = new List<IInfluencedByTime>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - timeOffset >= currentTime + 1 && loopRunning)
        {
            currentTime += 1;
            
            Debug.Log(currentTime);
        }

        if (currentTime >= loopDuration && !transitioning)
        {
            Debug.Log("Firing!");
            transitioning = true;
            StartCoroutine(EndLoop());
        }
    }

    public void PauseTime()
    {
        foreach (var influenced in influencedObjects)
        {
            influenced.timePause();
        }
        timestampAtPause = Time.timeSinceLevelLoad;
        loopRunning = false;
    }

    public void ResumeTime()
    {
        foreach (var influenced in influencedObjects)
        {
            influenced.timeResume();
        }
        timeOffset += Time.timeSinceLevelLoad - timestampAtPause;
        loopRunning = true;
    }

    private void Tick()
    {
        foreach (var influenced in influencedObjects)
        {
            influenced.tick(currentTime);
        }
    }

    public void SubscribeToTimeEvents(IInfluencedByTime influenced)
    {
        influencedObjects.Add(influenced);
    }

    public void EndTimeEarly()
    {
        PauseTime();
        loopRunning = false;
        transitioning = true;
        StartCoroutine(EndLoop());
    }

    private IEnumerator EndLoop()
    {
        transition.SetTrigger("EndTime");

        yield return new WaitForSeconds(3);
        Debug.Log("Switching scenes...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
