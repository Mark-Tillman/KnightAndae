using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownToForest : MonoBehaviour
{
    GameObject player;
    GameObject levelChange;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        levelChange = GameObject.Find("Level_Change");
        levelChange.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("ArmoredGoblin") == null)
        {
            levelChange.SetActive(true);
            //Debug.Log("Boss exists");
        }

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("On to the next Level");
        if (collision.tag == "Player")
            SceneManager.LoadScene("PlainsScene");
    }
}
