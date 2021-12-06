using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuController : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public CheckpointManager checkpointManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        Debug.Log("Resume game");
        Resume();
    }

    public void SaveGame()
    {
        Debug.Log("Save game");
        PlayerPrefs.SetInt("checkpointNumber", checkpointManager.getCheckpointNumber());
        PlayerPrefs.SetInt("arrowCount", checkpointManager.getLastArrowCount());
        PlayerPrefs.SetInt("sceneNumber", SceneManager.GetActiveScene().buildIndex);

        Debug.Log("Checkpoint: " + PlayerPrefs.GetInt("checkpointNumber") + " Arrows: " + PlayerPrefs.GetInt("arrowCount") + " Scene: " + PlayerPrefs.GetInt("sceneNumber"));
    }

    public void Settings()
    {
        Debug.Log("Settings");

    }

    public void MainMenu()
    {
        Debug.Log("MainMenu");
        Resume();
        SceneManager.LoadScene(0);

    }
}
