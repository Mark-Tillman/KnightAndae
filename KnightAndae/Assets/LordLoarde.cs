using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordLoarde : MonoBehaviour
{
    bool canAttack = true;
    public GameObject attackPoint;
    public GameObject teleportPoints;

    public EnemyAIv2 ai;

    float currentHealth;
    float lastHealth;
    public Animator spriteAnimator;
    int currentTeleportPoint;

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

        if(lastHealth > currentHealth)
        {
            StartCoroutine(Teleport());
        }

        if(ai.playerDetected)
            Debug.Log("Shoot");

        lastHealth = currentHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {

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
             
            if(canAttack)
            {
                spriteAnimator.SetTrigger("Attack");
                attackPoint.GetComponent<Animator>().SetTrigger("TrailAttack");
                StartCoroutine(TrailAttackCooldown());
            }
                
        }
    }

    IEnumerator TrailAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1.5f);
        canAttack = true;
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
        }
    }
}
