using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public PlayerInput playerInput;
    public GameObject firstSelectedButton;

    void Start()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
        playerInput.SwitchCurrentActionMap("WalkingOnLand");
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f; //here the time stuff
        GameIsPaused = false;
        playerInput.SwitchCurrentActionMap("WalkingOnLand");
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f; //here the time stuff
        GameIsPaused = true;
        playerInput.SwitchCurrentActionMap("UI");
        
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void LoadMenu()
    {
        GameIsPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
