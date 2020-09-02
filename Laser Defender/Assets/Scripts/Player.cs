using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.5f;
    [SerializeField] int health = 200;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.5f;

    [Header("Effects")]
    [SerializeField] ParticleSystem explosionPrefab;
    [SerializeField] AudioClip deathSFX;    
    [SerializeField] AudioClip[] shootSFX;
    [Range(0f, 1f)] [SerializeField] float volumeDeathSFX = 0.4f;
    [Range(0f, 1f)] [SerializeField] float volumeShootSFX = 0.4f;

    Coroutine firingCoroutine;
    
    float xMin;
    float xMax;
    float yMin;
    float yMax;
        
    void Start()
    {
        SetUpMoveBoundaries();
    }
    
    void Update()
    {
        Move();
        Fire();        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {       
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);        
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.ProcessHit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {        
        ParticleSystem vfx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        vfx.Play();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, volumeDeathSFX);
        FindObjectOfType<SceneLoader>().LoadGameOver();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());            
        }        
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laserProjecile = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laserProjecile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX[UnityEngine.Random.Range(0, shootSFX.Length)], Camera.main.transform.position, volumeShootSFX);
            yield return new WaitForSeconds(projectileFiringPeriod);
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
    
    public int GetHealth()
    {
        return health;
    }
}
