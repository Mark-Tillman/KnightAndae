using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordLoarde : MonoBehaviour
{
    bool canTrailAttack = true;
    bool canShoot = false;
    bool shootingPhase = false;
    bool canTeleport = false;
    public GameObject attackPoint;
    public GameObject teleportPoints;
    public GameObject player;

    public EnemyAIv2 ai;

    float currentHealth;
    float lastHealth;
    public Animator spriteAnimator;
    int currentTeleportPoint;
    public float shootCooldown = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = ai.maxHealth;
        lastHealth = ai.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = ai.totalHealth;

        if(currentHealth <= 13)
        {
            shootingPhase = true;
            canTeleport = true;
            ai.speed = 0;
            shootCooldown = 0.5f;
        }
        if(currentHealth <= 7)
        {
            ai.speed = 400;
            shootCooldown = 0.25f;
        }
            

        if(lastHealth > currentHealth && canTeleport)
        {
            //Debug.Log("Teleporting");
            StartCoroutine(Teleport());
        }

        if(ai.playerDetected && canShoot && shootingPhase)
        {
            StartCoroutine(ShootCoolDown());
            attackPoint.GetComponent<LordCombat>().shootProjectile();
        }

        if(Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 2.3f)) < 4.5 && canTrailAttack)
        {
            StartCoroutine(StartTrailAttack(player.GetComponent<CapsuleCollider2D>()));
            StartCoroutine(TrailAttackCooldown());
        }
        lastHealth = currentHealth;
    }

    IEnumerator StartTrailAttack(Collider2D other)
    {
        canTrailAttack = false;
        spriteAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.8f);
        Vector3 direction = other.transform.position - transform.position;
            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if(direction.x > 0)
                    attackPoint.transform.eulerAngles = new Vector3(0,0,0);
                 else if(direction.x < 0)
                    attackPoint.transform.eulerAngles = new Vector3(0,0,180);
            }
            else
            {
                if(direction.y > 0)
                    attackPoint.transform.eulerAngles = new Vector3(0,0,90);
                else if(direction.y < 0)
                    attackPoint.transform.eulerAngles = new Vector3(0,0,-90);
            }
        attackPoint.GetComponent<Animator>().SetTrigger("TrailAttack");
    }

    IEnumerator TrailAttackCooldown()
    {
        canTrailAttack = false;
        yield return new WaitForSeconds(1f);
        canTrailAttack = true;
    }

    IEnumerator Teleport()
    {
        {
            spriteAnimator.SetTrigger("disappear");
            int randomNum = Random.Range(0, 3);
            while(randomNum == currentTeleportPoint)
                randomNum = Random.Range(0, 3);

            yield return new WaitForSeconds(0.3f);
            transform.position = teleportPoints.transform.GetChild(randomNum).transform.position;
            currentTeleportPoint = randomNum;
            canShoot = true;
            canTrailAttack = false;
        }
    }

    IEnumerator ShootCoolDown()
    {
        {
            canShoot = false;
            yield return new WaitForSeconds(shootCooldown);
            canShoot = true;
        }
    }

    public void reset()
    {
        shootingPhase = false;
        canTeleport = false;
        ai.speed = 350;
        canTrailAttack = true;
    }

}
