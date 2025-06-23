using UnityEngine;
using Zenject;
using System.Collections.Generic;

/// <summary>
/// Movement behaviour for the Flying Eye enemy.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class FlyingEyeMovementMB : MonoBehaviour, IMoveBehaviour<FlyingEyeEnemy>
{
    [Inject] private FlyingEyeEnemy _enemy;

    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float flightSpeed = 3f;
    [SerializeField] private float waypointReachedDistance = 0.1f;

    private IPhysicsAdapter _physics;
    private IAnimatorAdapter _animator;

    private int _waypointIndex = 0;
    private Transform _currentWaypoint;

    private void Awake()
    {
        _physics = new RigidbodyAdapter(GetComponent<Rigidbody2D>());
        _animator = new AnimatorAdapter(GetComponent<Animator>());

        if (waypoints != null && waypoints.Count > 0)
        {
            _currentWaypoint = waypoints[_waypointIndex];
        }
    }

    public void Move(float _)
    {
        if (!_enemy.IsAlive || !_animator.GetBool(AnimationStrings.canMove) || waypoints == null || waypoints.Count == 0)
        {
            _physics.SetVelocity(Vector2D.Zero);
            return;
        }

        if (_currentWaypoint == null)
        {
            _physics.SetVelocity(Vector2D.Zero);
            return;
        }

        // Convert Unity positions to domain-safe vectors
        Vector2D currentPos = transform.position.ToDomain();
        Vector2D waypointPos = _currentWaypoint.position.ToDomain();

        Vector2D direction = (waypointPos - currentPos).Normalized;

        _physics.SetVelocity(direction * flightSpeed);
        FaceDirection(direction.X);

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
