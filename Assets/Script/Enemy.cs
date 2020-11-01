using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private float randomTime;
    [SerializeField] private float minTimeBetweenShot = 0.2f;
    [SerializeField] private float maxTimeBetweenShot = 2f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float explosionDestroy = 1f;

    [SerializeField] private GameObject enemyLaser;
    [SerializeField] private GameObject deathVFX;

    void Start()
    {
        randomTime = Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
        
    }

    void Update()
    {
        ShootTimer();
    }

    private void ShootTimer()
    {
        randomTime -= Time.deltaTime;
        if (randomTime <= 0)
        {
            Fire();
            randomTime = Random.Range(minTimeBetweenShot, maxTimeBetweenShot);
        }
    }

    private void Fire()
    {
        var laserEnemy =
            Instantiate(enemyLaser, transform.position, Quaternion.identity);
        laserEnemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        EnemyHit(damageDealer);
    }

    private void EnemyHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        var explosionVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explosionVFX , explosionDestroy);
    }
}