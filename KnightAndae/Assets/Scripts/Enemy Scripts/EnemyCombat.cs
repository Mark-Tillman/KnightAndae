using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    Animator animator;
    EnemyAIv2 enemyAi;
    PlayerHealth playerHealth;
    public GameObject attackPoint;
    public int attackDamage = 1;
    bool attacking = false;
    public float attackSpeed = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        enemyAi = GetComponentInParent<EnemyAIv2>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frameadw
    void Update()
    {
        //If within range
        if (enemyAi.canAttack && !attacking)
        {
            
            StartCoroutine(Attack());
        }
        
    }

    IEnumerator Attack()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
            enemyAi.canAttack = false;
            yield return new WaitForSeconds(attackSpeed);
            attacking = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Damage by: " + collision);
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
