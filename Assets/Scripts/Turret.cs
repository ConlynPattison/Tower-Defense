using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform _target;
    private Enemy _targetEnemy;
    
    [Header("General")]
    public float range = 15f;
    
    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float _fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowPct = 0.5f;
    
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;

    void Start()
    {
        // invokes a repeating function to be done every 0.5 seconds
        InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            _target = nearestEnemy.transform;
            _targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            _target = null;
        }
    }

    private void Update()
    {
        if (_target == null)
        {
            if (useLaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactEffect.Stop();
                impactLight.enabled = false;
            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (_fireCountdown <= 0f)
            {
                Shoot();
                _fireCountdown = 1f / fireRate;
            }

            _fireCountdown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        _targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        _targetEnemy.Slow(slowPct);
        
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, _target.position);

        Vector3 dir = firePoint.position - _target.position;

        impactEffect.transform.position = _target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }
    
    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        
        if (bullet != null)
            bullet.Seek(_target);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
