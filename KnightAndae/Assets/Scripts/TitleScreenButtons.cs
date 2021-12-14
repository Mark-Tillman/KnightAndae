using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenButtons : MonoBehaviour
{
    public string startLevel;
    public string levelSelect;

    public void NewGame()
    {
        SoundManager.PlaySound("confirm");
        //Application.LoadLevel(startLevel);
    }

    //public void LevelSelect()
    //{
       // Application.LoadLevel(levelSelect);
    //}

    public void QuitGame()
    {
        Application.Quit();
    }

}
