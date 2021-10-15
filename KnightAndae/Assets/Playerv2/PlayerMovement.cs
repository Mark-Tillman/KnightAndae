using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    //Animator variables
    public Animator animator;
    float lastX;
    float lastY;

    void Start()
    {
        //thisAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        AnimationUpdate();
    }

    void FixedUpdate()
    {  

        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        if (gameObject.GetComponent<Rigidbody2D>().velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (gameObject.GetComponent<Rigidbody2D>().velocity.x < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void AnimationUpdate()
    {
        

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
    }
}
