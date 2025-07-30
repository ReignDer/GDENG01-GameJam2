using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject pauseScreen;
    
    bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputReader.Escape += PauseScreen;
        pauseScreen.SetActive(false);
    }

    void OnDestroy()
    {
        inputReader.Escape -= PauseScreen;
    }
    private void PauseScreen(bool isKeyPressed)
    {
        if (isKeyPressed && !isPaused)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (isKeyPressed && isPaused)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }
    

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
