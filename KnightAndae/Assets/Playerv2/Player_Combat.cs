using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Animator animator;
    GameObject player;
    float knockBack = 200f;
    float damage = 1f;
    float attackCooldown = 0.1f;
    Vector2 oppositeDirection;
    public int weaponID;
    bool attacking = false;
    public GameObject arrowProjectile;
    float arrowSpeed = 2000f;
    Vector3 playerHeight = new Vector3(0, 1, 0);

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(Attack());
        }
    }

    public void setStats(float dam, float knock, float cooldown)
    {
        damage = dam;
        knockBack = knock;
        attackCooldown = cooldown;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("HIT");
            oppositeDirection = (other.transform.position - player.transform.position).normalized;
            other.gameObject.GetComponent<EnemyAIv2>().startGetAttacked(knockBack, oppositeDirection, damage);
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    public IEnumerator ShootArrow(Transform player)
    {
        GameObject arrow = Instantiate(arrowProjectile, player.position + playerHeight, player.rotation, player) as GameObject;
        yield return new WaitForSeconds(0.2f);
        arrow.GetComponent<Rigidbody2D>().simulated = true;
        arrow.GetComponent<Rigidbody2D>().AddForce(Vector2.right * arrowSpeed * player.localScale.x);
        arrow.transform.parent = null;
    }
}
