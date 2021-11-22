using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerHealth : MonoBehaviour
{
    GameObject player;


    public int maxHealth = 4;
    public int currentHealth;

    public HealthBar healthBar;
    public CheckpointManager checkpoints;
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
        if(currentHealth <= 0)
        {
            checkpoints.respawn();
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            //collision.gameObject.SendMessage("TakeDamage", 1);
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log(damage);
        if (isInvincible) return;
        
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }

        healthBar.SetHealth(currentHealth);

        StartCoroutine(BecomeTemporarilyInvincible());

        //Apply knockback

    }

    public void heal(int heal)
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += heal;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthBar.SetHealth(currentHealth);
            //Debug.Log(currentHealth);
        }
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
