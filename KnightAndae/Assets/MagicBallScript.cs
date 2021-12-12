using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBallScript : MonoBehaviour
{
    int damage = 1;
    public float knockback = 1f;
    public float stunTime = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBall());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 oppositeDirection;

        if (collision.tag != "Enemy" && collision.isTrigger == false)
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().startGetStunned(stunTime);
            oppositeDirection = (collision.transform.position - gameObject.transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(knockback * oppositeDirection, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    IEnumerator DestroyBall()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
