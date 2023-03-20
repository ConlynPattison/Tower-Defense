using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public int health = 100;

    public int value = 50;

    public GameObject deathEffect;
    
    private Transform _target;
    private int _wavepointIndex = 0;

    private void Start()
    {
        _target = Waypoints.points[0];
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {    
        PlayerStats.Money += value;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        
        Destroy(gameObject);
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
            EndPath();
            return;
        }

        _wavepointIndex++;
        _target = Waypoints.points[_wavepointIndex];
    }

    private void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
