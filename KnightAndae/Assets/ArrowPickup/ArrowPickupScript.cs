using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickupScript : MonoBehaviour
{
    Player_Combat combat;
    public int arrowAmount = 4;
    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindWithTag("PlayerHitBox").GetComponent<Player_Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject);
            combat.addArrow(arrowAmount);
        }
    }
}
