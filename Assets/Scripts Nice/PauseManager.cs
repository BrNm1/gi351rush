using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Button pauseButton;
    public Button resumeButton;
    public Button exitButton;
    public GameObject pauseMenu;
    public bool isPaused = false;
    
    private int random;

    void Start()
    {
        pauseMenu.SetActive(false);
        //pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(QuitGame);
        random = Random.Range(2, 8);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    void QuitGame()
    {
        SceneManager.LoadSceneAsync(random);
    }
}
