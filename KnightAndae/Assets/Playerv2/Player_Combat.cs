using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    GameObject player;
    public float knockBack = 500000;
    Vector2 oppositeDirection;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            oppositeDirection = (player.transform.position - other.transform.position).normalized;
            other.gameObject.GetComponent<EnemyAIv2>().getAttacked(knockBack, oppositeDirection);
        }
    }
}
