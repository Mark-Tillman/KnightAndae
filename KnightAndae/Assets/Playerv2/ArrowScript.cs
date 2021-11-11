using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    float damage = 1;
    float knockback = 100;
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

        if (collision.tag != "Player")
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameObject.transform.parent = collision.transform;
        }

        if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("HIT");
            oppositeDirection = (collision.transform.position - gameObject.transform.position).normalized;
            collision.gameObject.GetComponent<EnemyAIv2>().startGetAttacked(knockback, oppositeDirection, damage);
        }
    }

    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
