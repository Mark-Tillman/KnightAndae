using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrowScript : MonoBehaviour
{
    int damage = 1;
    public float knockback = 100;
    public float stunTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyArrow());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 oppositeDirection;

        if (collision.tag != "Enemy")
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.transform.parent = collision.transform;
        }

        if (collision.CompareTag("Player"))
        {
            //Debug.Log("HIT");
            collision.GetComponent<PlayerMovement>().startGetStunned(stunTime);
            oppositeDirection = (collision.transform.position - gameObject.transform.position).normalized;
            oppositeDirection.y = 0;
            collision.GetComponent<Rigidbody2D>().AddForce(knockback * oppositeDirection, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
