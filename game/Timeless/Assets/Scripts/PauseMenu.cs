using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public PlayerInput playerInput;

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
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f; //here the time stuff
        GameIsPaused = true;
        playerInput.SwitchCurrentActionMap("UI");
    }

    public void LoadMenu()
    {
        Debug.Log("Loading main menu...");
        GameIsPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
