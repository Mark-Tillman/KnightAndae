using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        PlayerPrefs.SetInt("loadFromSave", 0);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGame()
    {
        //Debug.Log("Checkpoint: " + PlayerPrefs.GetInt("checkpointNumber") + " Arrows: " + PlayerPrefs.GetInt("arrowCount") + " Scene: " + PlayerPrefs.GetInt("sceneNumber"));
        PlayerPrefs.SetInt("loadFromSave", 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("sceneNumber"));
    }

    public void Settings()
    {
    }

    public void QuitGame()
    {

        Application.Quit();
    }
}
