using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Anim : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //IEnumerator AttackCoroutine()
  //  {
        //animator.SetBool("Attack", true);
       // yield return new WaitForSeconds(0.5f);
       // animator.SetBool("Attack", false);
 //   }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack();
           // StartCoroutine(AttackCoroutine());
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

   
    }

}
