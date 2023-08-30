using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
                //if game is paused it resumes
            }
            else
            {
                Pause();
                //if game not paused it pauses
            }
        }
    }

    public void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        //activates pause menu and pauses time in game
    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        Paused = false;
        //deactivates pause menu and continues time in game
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
        //the main menu button functionality
    }
    public void QuitGame()
    {
        Debug.Log("Game exited.");
        Application.Quit();
        //the quit button functionality
    }

}
