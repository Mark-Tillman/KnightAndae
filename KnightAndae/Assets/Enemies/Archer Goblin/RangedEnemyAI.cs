using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedEnemyAi : MonoBehaviour
{
    //Target that the enemy will track
    Transform target;

    public float speed = 400f; //Enemy Speed
    public float nextWaypointDistance = 1f; //Distance before enemy moves to next waypoint

    public Transform sprite; //Enemy sprite reference

    Path path; //Stores the path that the enemy is to follow
    int currentWaypoint = 0; //Current waypoint in path the enemy is following
    //bool reachedEndOfPath = false; //Bool for if the end of the path has been reached

    Vector3 originalPosition; //Save original position to pathfind home
    Vector3 lastKnownPosition; //Save last known position of player

    //Seeker componenet of enemy and rigidbody
    Seeker seeker; //Seeker script from A* attached to enemy
    public Rigidbody2D rb; //Reference to enemy rigid body

    Transform player; //Player position reference
    bool playerDetected = false; //Bool for if the player is detected or not
    float playerDistance = 0f; //Stores distance from enemy to player
    float playerYDistance = 0f; //Distance from player in Y direction
    public float detectRange = 10f; //Range between enemy and player in which player can be detected
    bool firstDetected = false; //Bool for if the player has just been detected

    bool checkLastPosition = false; //Bool to tell the enemy to check the last known position or not
    bool chase = false; //Bool to tell the enemy to chase the player
    bool atHome = false; //Bool to tell the enemy if it is at home or not
    bool stunned = false; //Bool to tell if the enemy is stunned or not
    public float stunDuration = 0.5f; //How long the enemy stays stunned for

    public float playerHeight = 1f; //Player height gets added to player position so enemy tracks towards center of player instead of the bottom
    public float minDistance = 2.5f; //Minimum distance that the enemy can get to the player
    public float minYDistance = 1f; //Distance enemy can attack from, this is so it can be lined up on the Y axis
    public bool canAttack = false; //Controls if the enemy can attack or not
    public float enemyHeight = 0f;

    public float maxHealth; //Max Health
    public float totalHealth; // Current total health

    public float spriteScale; //Scale of the sprite, this is used when flipping the sprite left or right to maintain scale
    bool spriteFlipped = false; //Keeps track of if the enemy has been flipped so it only flips when it needs to and not constantly

    float lastX;
    float lastY;
    float currentX;
    float currentY;
    float dirX; //Keep track of movement direction for animation
    float dirY; //Keep track of movement direction for animation

    Animator animator; //Animation control 

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>(); //Find the player and get transform
        seeker = GetComponent<Seeker>(); //Get seeker component from enemy
        rb = GetComponent<Rigidbody2D>(); //Get rigidbody component from enemy
        animator = GetComponentInChildren<Animator>();

        totalHealth = maxHealth; //Set starting total health to max

        originalPosition = rb.transform.position; //Set original position to the starting position

        InvokeRepeating("UpdatePath", 0f, 0.2f); //Continuously update the enemy path

        if (spriteScale > 0)
            spriteFlipped = true;
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && playerDetected)
        {
            //Start path towards player if detected
            seeker.StartPath(rb.position, new Vector2(player.position.x, player.position.y + playerHeight), OnPathComplete);
        }
        else if (seeker.IsDone() && checkLastPosition)
        {
            //Start path towards lastKnownPosition is the player was detected but then lost
            seeker.StartPath(rb.position, lastKnownPosition, OnPathComplete);
        }
        else if (seeker.IsDone())
        {
            //Start path towards originalPosition if no where else to go
            seeker.StartPath(rb.position, originalPosition, OnPathComplete);

        }
    }

    //When path is done being drawn, assign it to the enemy and set current waypoint at the start
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        playerDistance = Vector2.Distance(rb.position, new Vector2(player.position.x, player.position.y + playerHeight)); //Always keep track of distance between enemy and player
        playerYDistance = ((rb.position.y + enemyHeight) - (player.position.y + playerHeight)); //Calculate how far the enemy is in the Y direction

        detectPlayer(); //Attempt to detect the player

        if (playerDetected)
        {
            //Change sprite orientation to be facing player if detected
            if (rb.position.x < player.position.x && spriteFlipped)
            {
                sprite.localScale = new Vector3(-spriteScale, sprite.localScale.y, sprite.localScale.z);
                spriteFlipped = false;
            }
            else if (rb.position.x > player.position.x && !spriteFlipped)
            {
                sprite.localScale = new Vector3(spriteScale, sprite.localScale.y, sprite.localScale.z);
                spriteFlipped = true;
            }
        }

        if (!stunned)
        {
            if ((chase || !atHome) && (playerDistance >= minDistance)) //If player is detected, chase will be true, and if the enemy is not at original position, atHome will be false
            {
                pathFind(); //Move
                animator.SetBool("Moving", true);
            }
            else
            {
                animator.SetBool("Moving", false);
            }

            if (playerDistance <= minDistance && playerDetected) //When the enemy is close enough, it won't use A* pathfinding, but starting homing directly to player
            {
                if (Mathf.Abs(playerYDistance) < minYDistance) //if the enemy is with the Y range it is able to attack
                {
                    animator.SetBool("Moving", false);
                    canAttack = true; //Can attack if within distance
                }
                else //If not within range, move up or down to get in range.
                {
                    animator.SetBool("Moving", true);
                    canAttack = false;

                    if (playerYDistance < 0f)
                        rb.velocity = Vector2.up * speed * Time.deltaTime;
                    else if (playerYDistance > 0f)
                        rb.velocity = Vector2.down * speed * Time.deltaTime;
                }
            }
            else //While the enemy is out of range, it can't attack
                canAttack = false;

            getDirection();
            lastX = transform.position.x;
            lastY = transform.position.y;
        }
    }

    void pathFind()
    {
        if (path == null)
        {
            //Do nothing if there is no path
            Debug.Log("No Path");
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            //Check if the end of the path has been reached
            //reachedEndOfPath = true;
            checkLastPosition = false;
            chase = false;
            return;
        }
        else
        {
            //Not done with path yet
            //reachedEndOfPath = false;
        }


        //Set direction to next way point and set a force at the correct speed
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //Set the velocity so it moves along the path
        rb.velocity = force;

        //Calculate distance from current position to the current waypoint 
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            //When close enough to the waypoint, start moving to the next one
            currentWaypoint++;
        }

    }

    void detectPlayer()
    {
        //Mask for the enemy to ignore - so that raycast doesn't collide with enemy casting it.
        LayerMask mask = LayerMask.GetMask("Enemy");

        //Cast a ray from the enemy to the direction of the player. ignore the enemy mask to avoid ray collision with self
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + enemyHeight), new Vector2(player.transform.position.x - transform.position.x, (player.transform.position.y - transform.position.y - enemyHeight) + playerHeight), detectRange, ~mask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + enemyHeight), new Vector2(player.transform.position.x - transform.position.x, (player.transform.position.y - transform.position.y - enemyHeight) + playerHeight), Color.red); //Shows what the enemy sees (for testing purposes)


        //Check if enemy sees the player unobstructed and within range
        if ((hit && hit.collider.gameObject.tag == "Player" && playerDistance <= detectRange))
        {
            playerDetected = true; //Player has been detected
            checkLastPosition = false; //Enemy does not need to check last known position while the player is detected
            chase = true; //Enemy should chase the player while detected
            firstDetected = true; //The enemy has just detected the player
            atHome = false; //The enemy will start chasing the player and will no longer be at home
            lastKnownPosition = player.transform.position; //Whereever the player is detected is the last known position
        }
        else //Player is out of range or has been obstructed
        {
            playerDetected = false; //Player no longer detected
        }

        if (firstDetected && !playerDetected) //If the player was detected but is no longer detected
        {
            checkLastPosition = true; //The enemy should check the last known position
            firstDetected = false; //Enemies original detection is false now
        }

        if (!playerDetected && !checkLastPosition && !atHome) //Player not detected, and Enemy not going to check the last position, and Enemy is not at home (original position)
        {
            float distance = Vector2.Distance(rb.position, originalPosition); //Determine distance from enemy to original position
            if (distance <= 0.1f) //Enemy is at original position
            {
                atHome = true; //Enemy is considered atHome and should no longer move
            }
        }
    }

    public void startGetAttacked(float knockBack, Vector3 oppositeDirection, float damageTaken)
    {
        StartCoroutine(GetAttacked(knockBack, oppositeDirection, damageTaken));
    }

    IEnumerator GetAttacked(float knockBack, Vector3 oppositeDirection, float damageTaken)
    {
        stunned = true;
        rb.AddForce(knockBack * oppositeDirection, ForceMode2D.Impulse);
        totalHealth -= damageTaken;
        //Debug.Log("Damaged by: -" + damageTaken);
        if (totalHealth <= 0)
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(stunDuration);
        stunned = false;
    }

    void getDirection()
    {

        currentX = transform.position.x;
        currentY = transform.position.y;

        //Debug.Log("X vel: " + rb.velocity.x + " Y vel: " + rb.velocity.y);
        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
        {
            dirX = 1;
            dirY = 0;
        }
        else if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(rb.velocity.y))
        {
            //Debug.Log(currentY > lastY);
            if ((currentY > lastY) && rb.velocity.y > 0)
            {
                dirX = 0;
                dirY = 1;
            }
            else if ((currentY < lastY) && rb.velocity.y < 0)
            {
                dirX = 0;
                dirY = -1;
            }

        }

        animator.SetFloat("dirX", dirX);
        animator.SetFloat("dirY", dirY);
    }
}
