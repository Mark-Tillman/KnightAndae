using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{

    public int healAmount = 1;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Created");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && collision.transform.GetComponent<PlayerHealth>().currentHealth < collision.transform.GetComponent<PlayerHealth>().maxHealth)
        {
            
            //Debug.Log("Destroyed");
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
            collision.transform.GetComponent<PlayerHealth>().heal(healAmount);
        }
    }
}
