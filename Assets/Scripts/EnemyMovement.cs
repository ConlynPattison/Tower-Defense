using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
    private int _wavepointIndex = 0;

    private Enemy _enemy;
    private NavMeshAgent _navAgent;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _target = Waypoints.points[0];
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.SetDestination(_target.position);
    }
    
    private void Update()
    {
        // Vector3 dir = _target.position - transform.position;
        // transform.Translate(_enemy.speed * Time.deltaTime * dir.normalized, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 2f)
        {
            GetNextWaypoint();
        }

        _navAgent.speed = _enemy.startSpeed;
        // _enemy.speed = _enemy.startSpeed;
    }

    private void GetNextWaypoint()
    {
        if (_wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        _wavepointIndex++;
        _target = Waypoints.points[_wavepointIndex];
        _navAgent.SetDestination(_target.position);
    }

    private void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
