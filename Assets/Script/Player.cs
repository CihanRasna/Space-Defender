using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Setting")] [SerializeField]
    private int health = 300;

    [SerializeField] private float shipSpeed = 10f;
    [SerializeField] private float padding = 1f;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float firingPeriod = 0.2f;

    [Header("Projectile Settings")] [SerializeField]
    private GameObject laserPrefab;

    [Header("Sounds/Effects Setting")] [SerializeField]
    private AudioClip deathSound;

    [SerializeField] [Range(0, 1)] private float deathVolume = 0.8f;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] [Range(0, 1)] private float shootVolume = 0.4f;


    private float xMin, xMax, yMin, yMax;
    private IEnumerator fireRepeatly;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        fireRepeatly = FireRepeat();
        MovementLimits();
    }

    private void Update()
    {
        Move();
        PlayerFire();
    }

    private void PlayerFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(fireRepeatly);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireRepeatly);
        }
    }

    private IEnumerator FireRepeat()
    {
        while (true)
        {
            var laser =
                Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            if (!(Camera.main is null))
                AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootVolume);
            yield return new WaitForSeconds(firingPeriod);
        }

        // ReSharper disable once IteratorNeverReturns
    }

    private void Move()
    {
        var xSpeed = Input.GetAxis("Horizontal") * shipSpeed * Time.deltaTime;
        var ySpeed = Input.GetAxis("Vertical") * shipSpeed * Time.deltaTime;

        var position = transform.position;

        var xPos = Mathf.Clamp(position.x + xSpeed, xMin, xMax);
        var yPos = Mathf.Clamp(position.y + ySpeed, yMin, yMax);

        position = new Vector2(xPos, yPos);

        transform.position = position;
    }

    private void MovementLimits()
    {
        var mainCamera = Camera.main;
        if (mainCamera is null) return;
        xMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }

        PlayerHit(damageDealer);
    }

    private void PlayerHit(DamageDealer damageDealer)
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
        if (!(mainCam is null))
            AudioSource.PlayClipAtPoint(deathSound, mainCam.transform.position, deathVolume);
        FindObjectOfType<LevelController>().LoadGameOverScene();
    }

    public int GetHealth()
    {
        if (health < 0)
        {
            return health = 0;
        }

        return health / 100;
    }
}