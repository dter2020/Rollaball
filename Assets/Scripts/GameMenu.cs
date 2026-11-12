using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject resumeButton;

    private bool paused;
    private bool roundEnded;

    private void Awake()
    {
        menuPanel.SetActive(false);
    }

    private void Update()
    {
        if (roundEnded || Keyboard.current == null ||
            !Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            return;
        }

        if (paused)
        {
            Resume();
        }
        else
        {
            paused = true;
            Time.timeScale = 0f;
            menuPanel.SetActive(true);
        }
    }

    public void Resume()
    {
        if (roundEnded)
        {
            return;
        }

        paused = false;
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowEndMenu()
    {
        roundEnded = true;
        resumeButton.SetActive(false);
        menuPanel.SetActive(true);
    }
}