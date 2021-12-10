using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordLoarde : MonoBehaviour
{

    public GameObject attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {

            Vector3 direction = other.transform.position - transform.position;
            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if(direction.x > 0)
                    attackPoint.transform.eulerAngles = new Vector3(0,0,0);
                 else if(direction.x < 0)
                    attackPoint.transform.eulerAngles = new Vector3(0,0,180);
            }
            else
            {
                if(direction.y > 0)
                    attackPoint.transform.eulerAngles = new Vector3(0,0,90);
                else if(direction.y < 0)
                    attackPoint.transform.eulerAngles = new Vector3(0,0,-90);
            }
             

            /*
            if(other.transform.position.x > transform.position.x)
                Debug.Log("Right Side");
            else if(other.transform.position.x < transform.position.x)
                Debug.Log("Left Side");
            else if(other.transform.position.y > transform.position.y)
                Debug.Log("Top Side");
                else if(other.transform.position.y < transform.position.y)
                Debug.Log("Bottom Side");
            */

            attackPoint.GetComponent<Animator>().SetTrigger("TrailAttack");
        }
    }
}
