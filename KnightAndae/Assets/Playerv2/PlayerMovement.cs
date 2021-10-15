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
    float lastX, lastY;

    void Start()
    {
        //thisAnim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        animator.SetFloat("lastX", horizontal);
        animator.SetFloat("lastY", vertical);

        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        AnimationUpdate();

        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }



        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void AnimationUpdate()
    {
        animator.SetFloat("xSpeed", horizontal);
        animator.SetFloat("ySpeed", horizontal);

        if(horizontal == 0 && vertical == 0)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }
    }
}
