using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordeCombat : MonoBehaviour
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
    public GameObject teleport1;
    public GameObject teleport2;
    public GameObject teleport3;
    public GameObject teleport4;
    public GameObject teleport5;
    public int nextTeleport = 1;

    public GameObject LordeWeapon;
    float arrowSpeed = 2000f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        enemyAi = GetComponentInParent<EnemyAIv2>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        //If within range
        if (enemyAi.canAttack && !attacking)
        {
            StartCoroutine(Attack());

            StartCoroutine(Teleport());
        }
    }

    IEnumerator Attack()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
            enemyAi.canAttack = false;
            StartCoroutine(ShootSkull());
            yield return new WaitForSeconds(attackSpeed);
            attacking = false;
        }
    }

    IEnumerator Teleport()
    {
        enemyAi.canAttack = false;
        animator.SetTrigger("TeleportStart");
        yield return new WaitForSeconds(2);
        switch (nextTeleport)
        {
            case 0:
                break;
            case 1:
                attackPoint.transform.parent.transform.position = teleport1.transform.position;
                nextTeleport = nextTeleport + 1;
                break;
            case 2:
                attackPoint.transform.parent.transform.position = teleport2.transform.position;
                nextTeleport = nextTeleport + 1;
                break;
            case 3:
                attackPoint.transform.parent.transform.position = teleport3.transform.position;
                nextTeleport = nextTeleport + 1;
                break;
            case 4:
                attackPoint.transform.parent.transform.position = teleport4.transform.position;
                nextTeleport = nextTeleport + 1;
                break;
            case 5:
                attackPoint.transform.parent.transform.position = teleport5.transform.position;
                nextTeleport = 1;
                break;
        }
        animator.SetTrigger("TeleportEnd");
        enemyAi.canAttack = true;
    }
    public IEnumerator ShootSkull()
    {
        GameObject arrow = Instantiate(LordeWeapon, transform) as GameObject;
        yield return new WaitForSeconds(0.8f);
        arrow.GetComponent<Rigidbody2D>().simulated = true;
        arrow.GetComponent<Rigidbody2D>().AddForce(Vector2.left * arrowSpeed * transform.parent.localScale.x);
        arrow.transform.parent = null;
    }

    public void reset()
    {
        attacking = false;
        if (gameObject.transform.Find("LordeWeapon(Clone)") != null)
        {
            Destroy(gameObject.transform.Find("LordeWeapon(Clone)").gameObject);
        }
    }
}