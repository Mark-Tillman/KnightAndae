using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordCombat : MonoBehaviour
{
    PlayerHealth playerHealth;
    public GameObject attackPoint;
    public int attackDamage = 1;
    public float attackSpeed = 4f;
    public float knockback = 1000f;
    public float stunTime = 1f;
    public GameObject projectile;
    float projectileSpeed = 700f;
    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            shootProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerMovement>().startGetStunned(stunTime);
            Vector2 oppositeDirection = (collision.transform.position - gameObject.transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(knockback * oppositeDirection, ForceMode2D.Impulse);
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void shootProjectile()
    {
        Vector2 playerDirection = (new Vector2(player.transform.position.x, (player.transform.position.y)) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)).normalized;
        GameObject ball= Instantiate(projectile, transform) as GameObject;
        if(ball != null)
        {
            ball.GetComponent<Rigidbody2D>().simulated = true;
            ball.GetComponent<Rigidbody2D>().AddForce(playerDirection * projectileSpeed * transform.parent.localScale.x);
            ball.transform.parent = null;
        }
        
    }
}
