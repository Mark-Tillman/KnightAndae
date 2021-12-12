using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 15.0f;

    //Animator variables
    public Animator animator;
    float lastX;
    float lastY;
    public float currentX;
    public float currentY;

    int currentWeaponID;
    Player_Combat combat;
    public CheckpointManager checkpoints;

    public bool canMove = true;
    //public bool attacking = false;


    void Start()
    {
        //thisAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        combat = GetComponentInChildren<Player_Combat>();
        currentWeaponID = 1;
    }

    void Update()
    {
        // Gives a value between -1 and 1
        //horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        //vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if(Input.GetKey(KeyCode.W))
            vertical = 1;
        else if(Input.GetKey(KeyCode.S))
            vertical = -1;
        else
            vertical = 0;
        if(Input.GetKey(KeyCode.D))
            horizontal = 1;
        else if(Input.GetKey(KeyCode.A))
            horizontal = -1;
        else 
            horizontal = 0;

        if (canMove)
            AnimationUpdate();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }

            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

            if (!combat.attacking)
            {
                if (gameObject.GetComponent<Rigidbody2D>().velocity.x >= 0.01f)
                    transform.localScale = new Vector3(1f, 1f, 1f);

                if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0f)
                    transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (combat.attacking)
            {
                if(Input.GetKey(KeyCode.RightArrow) || (Input.mousePosition.y < ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y > ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height))
                    transform.localScale = new Vector3(1f, 1f, 1f);

                if(Input.GetKey(KeyCode.LeftArrow) || (Input.mousePosition.y > ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y < ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height))
                    transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }

    public void changeWeapon(int nextWeaponID)
    {
        animator.SetLayerWeight(currentWeaponID, 0);
        animator.SetLayerWeight(nextWeaponID, 1);
        currentWeaponID = nextWeaponID;
    }

    void AnimationUpdate()
    {
        //setCurrentXandY();
        //Debug.Log("CurrentX: " + animator.GetFloat("currentX") + " CurrentY: " + animator.GetFloat("currentY"));
        if (horizontal == 0f && vertical == 0f)
        {
            animator.SetFloat("lastX", lastX);
            animator.SetFloat("lastY", lastY);
            animator.SetBool("Moving", false);
        }
        else
        {
            lastX = Mathf.Abs(horizontal);
            lastY = vertical;
            animator.SetBool("Moving", true);
        }

        animator.SetFloat("xSpeed", Mathf.Abs(horizontal));
        animator.SetFloat("ySpeed", vertical);
        //Debug.Log("LastX: " + animator.GetFloat("lastX") + " LastY: " + animator.GetFloat("lastY"));
    }

    void setCurrentXandY() //Keep track of the current direction being face in order to attack in the correct direction.
    {
        if (/*!combat.attacking*/true)
        {
            if (Mathf.Abs(horizontal) > 0)
            {
                currentX = 1;
                currentY = 0;
            }
            else if (vertical < 0)
            {
                currentX = 0;
                currentY = -1;
            }
            else if (vertical > 0)
            {
                currentX = 0;
                currentY = 1;
            }
            animator.SetFloat("currentX", currentX);
            animator.SetFloat("currentY", currentY);
        }
    }

    public void startGetStunned(float stunTime)
    {
        StartCoroutine(GetStunned(stunTime));
    }
    public IEnumerator GetStunned(float stunTime)
    {
        canMove = false;
        yield return new WaitForSeconds(stunTime);
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            checkpoints.updateCheckpoint(collision.transform);
        }
    }

}
