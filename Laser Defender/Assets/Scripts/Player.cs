using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    
    float xMin;
    float xMax;
    float yMin;
    float yMax;
        
    void Start()
    {
        SetUpMoveBoundaries();
        StartCoroutine(MyCoroutine());
    }
    
    void Update()
    {
        Move();
        Fire();
    }

    IEnumerator MyCoroutine()
    {
        while (true)
        {
            Debug.Log("Print smth");
            yield return new WaitForSeconds(3f);
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject laserProjecile = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laserProjecile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        
        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        float clampedXPos = Mathf.Clamp(newXPos, xMin, xMax);
        float clampedYPos = Mathf.Clamp(newYPos, yMin, yMax);

        transform.position = new Vector2(clampedXPos, clampedYPos);                        
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
    
}
