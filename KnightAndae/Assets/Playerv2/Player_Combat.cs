using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    GameObject player;
    public float knockBack = 10000f;
    Vector2 oppositeDirection;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("HIT");
            oppositeDirection = (other.transform.position - player.transform.position).normalized;
            other.gameObject.GetComponent<EnemyAIv2>().startGetAttacked(knockBack, oppositeDirection, 1);
        }
    }
}
