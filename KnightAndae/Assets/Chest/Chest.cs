using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject loot;
    bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !opened)
        {
            opened = true;
            gameObject.GetComponent<Animator>().SetTrigger("open");
            Instantiate(loot, gameObject.transform.position, Quaternion.identity);
        }
    }

    public void reset()
    {
        opened = false;
        gameObject.GetComponent<Animator>().Play("closed");
    }
}
