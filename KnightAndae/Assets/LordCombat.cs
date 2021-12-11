using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordCombat : MonoBehaviour
{
    PlayerHealth playerHealth;
    public GameObject attackPoint;
    public int attackDamage = 1;
    bool attacking = false;
    public float attackSpeed = 4f;
    public float knockback = 10f;
    public float stunTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().startGetStunned(stunTime);
            //Debug.Log("Damage by: " + collision);
            Vector3 oppositeDirection = new Vector3(collision.transform.position.x - transform.position.x, 0, 0).normalized;
            oppositeDirection.y = 0;
            collision.GetComponent<Rigidbody2D>().AddForce(knockback * oppositeDirection, ForceMode2D.Impulse);
            playerHealth.TakeDamage(attackDamage);
 
        }
    }
}
