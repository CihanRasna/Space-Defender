using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float shipSpeed = 10f;
    private float xMin, xMax, yMin, Ymax;

    [SerializeField] private float padding = 1f;
    
    void Start()
    {
        MovementLimits();
    }

    private void MovementLimits()
    {
        Camera mainCamera = Camera.main;
        xMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        Ymax = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {
        var xSpeed = Input.GetAxis("Horizontal") * shipSpeed * Time.deltaTime;
        var ySpeed = Input.GetAxis("Vertical") * shipSpeed * Time.deltaTime;
        var xPos = Mathf.Clamp(transform.position.x + xSpeed, xMin, xMax);
        var yPos = Mathf.Clamp(transform.position.y + ySpeed, yMin, Ymax);
        
        transform.position = new Vector2(xPos,yPos);
        
    }
}
