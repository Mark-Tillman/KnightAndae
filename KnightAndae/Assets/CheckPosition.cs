using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckPosition : MonoBehaviour
{
    string sceneName;
    bool playing = false;
    void Update()
    {
        if (!playing)
        {
            sceneName = SceneManager.GetActiveScene().name;
            if (sceneName == "Castle5")
            {
                if (transform.position.y > -50 && MusicManager.GetMusic() != "boss")
                {
                    MusicManager curManager = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicManager>();
                    curManager.SwitchMusic("boss");
                    playing = true;
                }
            }
        }
    }
}
