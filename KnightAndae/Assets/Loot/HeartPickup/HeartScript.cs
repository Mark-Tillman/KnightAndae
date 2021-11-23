using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    bool on = false;
    public int healAmount = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PickupCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && collision.transform.GetComponent<PlayerHealth>().currentHealth < collision.transform.GetComponent<PlayerHealth>().maxHealth && collision.isTrigger == false && on)
        {
            Destroy(transform.parent.gameObject);
            Destroy(gameObject);
            collision.transform.GetComponent<PlayerHealth>().heal(healAmount);
        }
    }

    IEnumerator PickupCooldown()
    {
        yield return new WaitForSeconds(0.15f);
        on = true;
    }
}
