using UnityEngine;
using Zenject;
using System.Collections.Generic;
/// <summary>
/// Movement behaviour for the Flying Eye enemy.
/// </summary>
public class FlyingEyeMovementMB : MonoBehaviour, IMoveBehaviour<FlyingEyeEnemy>
{
    [Inject] private FlyingEyeEnemy _enemy;

    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float flightSpeed = 3f;
    [SerializeField] private float waypointReachedDistance = 0.1f;

    private Rigidbody2D _rb;
    private Animator _animator;

    private int _waypointIndex = 0;
    private Transform _currentWaypoint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentWaypoint = waypoints[_waypointIndex];
    }

    public void Move(float _)
    {
        if (!_enemy.IsAlive || !_animator.GetBool(AnimationStrings.canMove))
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (_currentWaypoint.position - transform.position).normalized;
        _rb.linearVelocity = direction * flightSpeed;
        FaceDirection(direction.x);

        if (Vector2.Distance(transform.position, _currentWaypoint.position) <= waypointReachedDistance)
        {
            _waypointIndex = (_waypointIndex + 1) % waypoints.Count;
            _currentWaypoint = waypoints[_waypointIndex];
        }
    }

    public void FaceDirection(float x)
    {
        if ((x < 0 && transform.localScale.x > 0) || (x > 0 && transform.localScale.x < 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
