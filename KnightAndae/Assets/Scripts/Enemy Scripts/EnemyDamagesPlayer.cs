using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagesPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject enemy;
    public float knockBack = 500000;
    Vector2 oppositeDirection;

    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            oppositeDirection = (enemy.transform.position - other.transform.position).normalized;
            //other.gameObject.GetComponent<EnemyAIv2>().getAttacked(knockBack, oppositeDirection);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
