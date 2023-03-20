using System;
using System.Net.Sockets;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;

    public float speed = 70f;

    public int damage = 50;
    
    public float explosionRadius = 0f;
    public GameObject impactEffect;

    public void Seek(Transform target)
    {
        _target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = Time.deltaTime * speed;

        if (dir.magnitude <= distanceThisFrame) // if we would overshoot after moving on this frame
        {
            HitTarget();
            return;
        }
        
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(_target);
    }

    void HitTarget()
    {
        GameObject effectInstance = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(_target);
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
