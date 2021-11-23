using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Combat : MonoBehaviour
{
    public Animator animator;
    GameObject player;
    float knockBack = 200f;
    float damage = 1f;
    float cooldown;
    Vector2 oppositeDirection;
    public int weaponID;
    public bool attacking = false;
    public GameObject arrowProjectile;
    float arrowSpeed = 2000f;
    int arrowCount = 10;
    public Text arrowText;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        arrowText.text = arrowCount.ToString();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(Attack());
            if(weaponID == 3)
                StartCoroutine(ShootArrow(player.transform));
        }
    }

    public void updateWeapon(int ID)
    {
        Debug.Log("Updating Stats");
        weaponID = ID;
        switch (weaponID)
        {
            case 0: // no weapon selected
                break;
            case 1:
                //Sword
                damage = 1;
                knockBack = 200;
                cooldown = 0.3f;
                break;
            case 2:
                //Spear
                damage = 0.75f;
                knockBack = 75;
                cooldown = 0.2f;
                break;
            case 3:
                //Bow
                damage = 1;
                knockBack = 200;
                cooldown = 0.5f;
                break;
            case 4:
                //Hammer
                damage = 1;
                knockBack = 500;
                cooldown = 0.7f;
                break;
            case 5:
                //Unarmed
                damage = 0;
                knockBack = 0;
                cooldown = 0;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            oppositeDirection = (other.transform.position - player.transform.position).normalized;
            other.gameObject.GetComponent<EnemyAIv2>().startGetAttacked(knockBack, oppositeDirection, damage);
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        yield return new WaitForSeconds(cooldown);
        attacking = false;
    }

    public IEnumerator ShootArrow(Transform player)
    {
        if (arrowCount > 0)
        {
            Vector3 arrowPosition = new Vector3(player.localScale.x * 0.6f, 1, 0);
            GameObject arrow = Instantiate(arrowProjectile, player.position + arrowPosition, player.rotation, player) as GameObject;
            yield return new WaitForSeconds(0.2f);
            arrow.GetComponent<Rigidbody2D>().simulated = true;
            arrow.GetComponent<Rigidbody2D>().AddForce(Vector2.right * arrowSpeed * player.localScale.x);
            arrow.transform.parent = null;
            arrowCount--;
            arrowText.text = arrowCount.ToString();
        }
    }

    public void addArrow(int num)
    {
        arrowCount += num;
        arrowText.text = arrowCount.ToString();
    }
}
