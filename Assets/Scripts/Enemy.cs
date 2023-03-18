using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform _target;
    private int _wavepointIndex = 0;

    private void Start()
    {
        _target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (_wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        _wavepointIndex++;
        _target = Waypoints.points[_wavepointIndex];
    }
}
