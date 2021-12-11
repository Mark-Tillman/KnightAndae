using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Start game");
        PlayerPrefs.SetInt("loadFromSave", 0);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        Debug.Log("Load game");
        Debug.Log("Checkpoint: " + PlayerPrefs.GetInt("checkpointNumber") + " Arrows: " + PlayerPrefs.GetInt("arrowCount") + " Scene: " + PlayerPrefs.GetInt("sceneNumber"));
        PlayerPrefs.SetInt("loadFromSave", 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("sceneNumber"));
    }

    public void Settings()
    {
        Debug.Log("Settings");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
