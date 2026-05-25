using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private int loopDuration = 20; // seconds
    [SerializeField]
    private AudioSource explosionAudioSource;

    private bool transitioning = false;
    private bool outroPlaying = false;

    [SerializeField]
    Animator transition; 

    void Update()
    {
        if (Time.timeSinceLevelLoad >= loopDuration - 5 && !outroPlaying){
            outroPlaying = true;
            explosionAudioSource.Play();
        }

        if (Time.timeSinceLevelLoad >= loopDuration && !transitioning)
        {
            Debug.Log("Firing!");
            transitioning = true;
            StartCoroutine(EndLoop());
        }
    }
    
    public void EndTimeEarly()
    {
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