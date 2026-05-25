using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour, IInteractable
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject endingCanvas;
    [SerializeField] private GameObject videoScreen;
    [SerializeField] private GameObject endScreenElements;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void Start()
    {
        endingCanvas.SetActive(false);
        endScreenElements.SetActive(false);
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    public bool Interact()
    {
        endingCanvas.SetActive(true);
        videoScreen.SetActive(true);
        videoPlayer.Play();
        
        return true; 
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        videoScreen.SetActive(false);
        endScreenElements.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void Dismiss()
    {
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}