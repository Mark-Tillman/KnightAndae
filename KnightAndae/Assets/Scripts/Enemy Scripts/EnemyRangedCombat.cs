using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedCombat : MonoBehaviour
{
    Animator animator;
    EnemyAIv2 enemyAi;
    PlayerHealth playerHealth;
    public GameObject attackPoint;
    public int attackDamage = 1;
    bool attacking = false;
    public float attackSpeed = 1.5f;
    public float knockback = 1f;
    public float stunTime = 0;

    public GameObject arrowProjectile;
    float arrowSpeed = 2000f;

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
            StartCoroutine(ShootArrow());
            yield return new WaitForSeconds(attackSpeed);
            attacking = false;
        }
    }

    public IEnumerator ShootArrow()
    {
        GameObject arrow = Instantiate(arrowProjectile, transform) as GameObject;
        yield return new WaitForSeconds(0.8f);
        arrow.GetComponent<Rigidbody2D>().simulated = true;
        arrow.GetComponent<Rigidbody2D>().AddForce(Vector2.left * arrowSpeed * transform.parent.localScale.x);
        arrow.transform.parent = null;
    }
}
