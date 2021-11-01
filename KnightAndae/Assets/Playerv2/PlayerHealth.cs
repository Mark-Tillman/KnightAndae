using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerHealth : MonoBehaviour
{
    GameObject player;


    public int maxHealth = 4;
    public int currentHealth;

    public HealthBar healthBar;

    private bool isInvincible = false;

    
    public float invincibilityDurationSeconds;
    
    public float delayBetweenInvincibilityFlashes;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // used for debugging health
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(1);
        //}
        //else if (Input.GetKeyDown(KeyCode.H))
        //{
        //    TakeDamage(-1);
        //}
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //collision.gameObject.SendMessage("TakeDamage", 1);
            //TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        StartCoroutine(BecomeTemporarilyInvincible());
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        //Debug.Log("Player turned invincible!");
        isInvincible = true;
        int timesLooped = 0;
        // Flash on and off for roughly invincibilityDurationSeconds seconds
        for (float i = 0; i < invincibilityDurationSeconds; i += delayBetweenInvincibilityFlashes)
        {
            // TODO: add flashing logic here
            if (timesLooped % 2 == 0)
            {
                player.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }

            timesLooped++;
            yield return new WaitForSeconds(delayBetweenInvincibilityFlashes);
        }
        player.GetComponent<SpriteRenderer>().enabled = true;
        //Debug.Log("Player is no longer invincible!");
        isInvincible = false;
    }
}
