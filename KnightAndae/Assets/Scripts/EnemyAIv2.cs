using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIv2 : MonoBehaviour
{
    //Target that the enemy will track
    Transform target;

    //Speed
    public float speed = 200f;
    public float nextWaypointDistance = 1f;

    public Transform sprite;    

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Vector3 originalPosition;

    //Seeker componenet of enemy and rigidbody
    Seeker seeker;
    Rigidbody2D rb;

    //Player detection
    Transform player;
    bool playerDetected = false;
    float playerDistance = 0f;
    public float detectRange = 10f;
    Vector3 lastKnownPosition;
    bool checkedLastPosition = false;
    bool firstDetected = false;
    public float detectBubble = 5;
    public float minimumDistance = 1;
    public float playerHeight = 1.5f;

    

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        originalPosition = rb.transform.position;
        lastKnownPosition = originalPosition;

        InvokeRepeating("UpdatePath", 0f, 0.2f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && playerDetected)
        {
            //Start path at current position and point it to player. OnPathComplete is called when path is done being drawn
            seeker.StartPath(rb.position, new Vector2(player.position.x, player.position.y + playerHeight), OnPathComplete);
        }
        else if(seeker.IsDone() && !checkedLastPosition)
        {
            seeker.StartPath(rb.position, lastKnownPosition, OnPathComplete);
            //checkedLastPosition = true;
        }
        else
        {
            //Start path at current position and point it to player. OnPathComplete is called when path is done being drawn
            seeker.StartPath(rb.position, originalPosition, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(rb.position, player.position);

        detectPlayer();

        if (!reachedEndOfPath)
        {
            pathFind();
        }
    }

    void pathFind()
    {
        if (path == null)
        {
            //Do nothing if there is no path
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count || playerDistance < minimumDistance)
        {
            //Check if the end of the path has been reached
            rb.velocity = new Vector2(0, 0);
            reachedEndOfPath = true;
            checkedLastPosition = true;
            return;
        }
        else
        {
            //Not done with path yet
            reachedEndOfPath = false;
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

        //Change sprite orientation based on movement direction.
        if (rb.velocity.x >= 0.01f)
        {
            sprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x < 0f)
        {
            sprite.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void detectPlayer()
    {
        //Mask for the enemy to ignore - so that raycast doesn't collide with enemy casting it.
        LayerMask mask = LayerMask.GetMask("Enemy");

        //Cast a ray from the enemy to the direction of the player. ignore the enemy mask to avoid ray collision with self
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(player.transform.position.x - transform.position.x, (player.transform.position.y - transform.position.y) + playerHeight), detectRange, ~mask);
        Debug.DrawRay(transform.position, new Vector2(player.transform.position.x - transform.position.x, (player.transform.position.y - transform.position.y) + playerHeight), Color.red); //Shows what the enemy sees (for testing purposes)


        //Check if enemy sees the player unobstructed and within range
        if ((hit && hit.collider.gameObject.tag == "Player" && playerDistance <= detectRange))
        {
            playerDetected = true;
            target = player;
            checkedLastPosition = false;
            reachedEndOfPath = false;
            firstDetected = true;
            lastKnownPosition = player.transform.position;
        }
        else if(playerDistance <= detectBubble && firstDetected)
        {
            playerDetected = true;
            target = player;
            checkedLastPosition = false;
            reachedEndOfPath = false;
        }
        else
        {
            //Debug.Log("Player out of range or obstructed");
            //rb.velocity = new Vector2(0, 0);
            playerDetected = false;
            //target = lastKnownPosition;
        }

    }
}
