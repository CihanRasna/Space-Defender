using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float shipSpeed = 10f;
    private float xMin, xMax, yMin, Ymax;
    
    [SerializeField] private GameObject laserPrefab;
    
    [SerializeField] private float padding = 1f;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float firingPeriod = 0.2f;
    
    
    private IEnumerator fireRepeatly;

    void Start()
    {
        fireRepeatly = FireRepeat();
        MovementLimits();
    }

    void Update()
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

    IEnumerator FireRepeat()
    {
        while (true)
        {
            GameObject laser = 
                Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0 ,projectileSpeed);
            yield return new WaitForSeconds(firingPeriod);
        }
    }

    private void Move()
    {
        var xSpeed = Input.GetAxis("Horizontal") * shipSpeed * Time.deltaTime;
        var ySpeed = Input.GetAxis("Vertical") * shipSpeed * Time.deltaTime;
        
        var position = transform.position;
        
        var xPos = Mathf.Clamp(position.x + xSpeed, xMin, xMax);
        var yPos = Mathf.Clamp(position.y + ySpeed, yMin, Ymax);
        
        position = new Vector2(xPos,yPos);
        
        transform.position = position;
    }
    
    private void MovementLimits()
    {
        var mainCamera = Camera.main;
        if (mainCamera is null) return;
        xMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        Ymax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
    
    
}
