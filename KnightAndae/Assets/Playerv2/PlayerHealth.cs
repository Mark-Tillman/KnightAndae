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
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) 
            return;
        SoundManager.PlaySound("hit");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            SoundManager.PlaySound("death");
            currentHealth = 0;
        }
        healthBar.SetHealth(currentHealth);
        StartCoroutine(BecomeTemporarilyInvincible());
    }

    public void heal(int heal)
    {
        SoundManager.PlaySound("health");
        if(currentHealth < maxHealth)
        {
            currentHealth += heal;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthBar.SetHealth(currentHealth);
        }
    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;
        int timesLooped = 0;
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
        isInvincible = false;
    }
}
