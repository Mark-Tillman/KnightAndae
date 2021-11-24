using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{

    GameObject player;
    GameObject levelChange;
    public GameObject boss;
    public string sceneName;
    bool bossDead = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        levelChange = GameObject.Find("Level_Change");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!boss.activeSelf)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            bossDead = true;
        }
        if(bossDead && Vector3.Distance(gameObject.transform.position, player.transform.position) < 10)
        {
            gameObject.GetComponent<Animator>().SetTrigger("openFridge");
        }
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("FirdgeOpened"))
        {
            SceneManager.LoadScene(sceneName);
        }
        
    }
}
