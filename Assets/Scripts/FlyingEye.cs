using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 3f;
    public float waypointReachedDistance=0.1f;
    public DetectionZone_old biteDetectionZone;
    public Collider2D deathCollider;
    public List<Transform> waypoints;
    public bool _hasTarget = false;

    Animator animator;
    Rigidbody2D rb;

    Damageable_old damageable;
    Transform nextWaypoint;
    int waypointNum = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable_old>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    void Update()
    {
        HasTarget= biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.linearVelocity = Vector2.zero; // Detiene el Rigidbody si no puede moverse
            }
        }

        
    }

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }



    private void Flight()
    {
        //fly to next waypoint
        Vector2 directionToWaypoint=(nextWaypoint.position - transform.position).normalized;

        // check if we have reached the waypoint already 
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.linearVelocity= directionToWaypoint * flightSpeed;
        UpdateDirection();
        // see if we need to switch waypoints
        if(distance<=waypointReachedDistance)
        {
            waypointNum++;
            if (waypointNum >= waypoints.Count)
            {
                waypointNum = 0; // Loop back to the first waypoint
            }
            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if (transform.localScale.x> 0) // facint right
        {
            if (rb.linearVelocity.x<0) 
            { //flip
                transform.localScale = new Vector3(
                    -1 * locScale.x, locScale.y, locScale.z
                    );
            }
        }
        else
        {
            if (rb.linearVelocity.x > 0)
            { 
                transform.localScale = new Vector3(
                    -1 * locScale.x, locScale.y, locScale.z
                    );
            }
        }
    }

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value); // Actualiza el Animator
        }
    }

    public void OnDeath()
    {
        // Handle death logic here, like playing an animation or destroying the object
        // dead flyier falls to the ground
        rb.gravityScale = 2f;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        deathCollider.enabled = true;

        Debug.Log("FlyingEye has died.");
    }
}
