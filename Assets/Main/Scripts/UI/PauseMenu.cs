using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;

    public GameObject healthBar;
    public GameObject oxygenBar;
    public GameObject pauseUI;
    public GameObject OptionUI;


    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else if (!isPaused)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {

        Cursor.lockState = CursorLockMode.Locked;
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
        healthBar.SetActive(true);
        oxygenBar.SetActive(true);
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseUI.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;
        healthBar?.SetActive(false);
        oxygenBar.SetActive(false);

    }

    public void Restart()
    {
        SceneManager.LoadScene("Assets/Main/Scenes/MainScene.unity");
    }

    public void Options()
    {
        if (isPaused)
        {
            OptionUI.SetActive(true);
            pauseUI.SetActive(false);
            isPaused = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
