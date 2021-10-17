using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAIv2 : MonoBehaviour
{
    //Target that the enemy will track
    public Transform target;

    //Speed
    public float speed = 200f;
    public float nextWaypointDistance = 1f;

    public Transform sprite;    

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    //Seeker componenet of enemy and rigidbody
    Seeker seeker;
    Rigidbody2D rb;
    
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            //Start path at current position and point it to player. OnPathComplete is called when path is done being drawn
            seeker.StartPath(rb.position, target.position, OnPathComplete);
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
        if(path == null)
        {
            //Do nothing if there is no path
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            //Check if the end of the path has been reached
            reachedEndOfPath = true;
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

        if(distance < nextWaypointDistance)
        {
            //When close enough to the waypoint, start moving to the next one
            currentWaypoint++;
        }

        //Change sprite orientation based on movement direction.
        if(rb.velocity.x >= 0.01f)
        {
            sprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(rb.velocity.x < 0f)
        {
            sprite.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
