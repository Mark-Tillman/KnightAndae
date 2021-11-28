using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickupScript : MonoBehaviour
{
    Player_Combat combat;
    bool on = false;
    public int arrowAmount = 4;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PickupCooldown());
        combat = GameObject.FindWithTag("PlayerHitBox").GetComponent<Player_Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && collision.isTrigger == false && on)
        {
            SoundManager.PlaySound("health");
            Destroy(gameObject);
            combat.addArrow(arrowAmount);
            Debug.Log("Adding Arrows");
        }
    }

    IEnumerator PickupCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        on = true;
    }
}
