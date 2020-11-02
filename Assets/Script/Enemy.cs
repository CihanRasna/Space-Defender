using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float health = 100f;
    [SerializeField] private float randomTime;
    [SerializeField] private float minTimeBetweenShot = 0.2f;
    [SerializeField] private float maxTimeBetweenShot = 2f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private int scoreValue = 100;
    

    [SerializeField] private GameObject enemyLaser;
    
    [Header("Sounds/Effects Setting")]
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] [Range(0, 1)] private float deathVolume = 0.8f;
    [SerializeField] private AudioClip enemyShootSound;
    [SerializeField] [Range(0, 1)] private float shootVolume = 0.2f;
    [SerializeField] private float explosionDestroy = 1f;
    
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
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
        if (!(mainCam is null))
            AudioSource.PlayClipAtPoint(enemyShootSound, mainCam.transform.position, shootVolume);
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
        FindObjectOfType<GameManager>().AddScore(scoreValue);
        Destroy(gameObject);
        var explosionVFX = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Destroy(explosionVFX, explosionDestroy);
        if (!(Camera.main is null))
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVolume);
    }
}