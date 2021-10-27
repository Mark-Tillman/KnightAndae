using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    Animator animator;
    EnemyAIv2 enemyAi;
    public GameObject attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    bool attacking = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyAi = gameObject.GetComponent<EnemyAIv2>();
    }

    // Update is called once per frame
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
            Debug.Log("Swinging");
            yield return new WaitForSeconds(1.5f);
            attacking = false;
        }
    }
}
