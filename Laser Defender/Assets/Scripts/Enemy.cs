using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scorePoints = 50;
    
    [Header("Shooting")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;    
    [SerializeField] float projectileSpeed = 10f;

    [Header("Effects")]
    [SerializeField] ParticleSystem explosionPrefab;
    [SerializeField] ParticleSystem impactVFX;
    [SerializeField] AudioClip shootSFX;    
    [SerializeField] AudioClip deathSFX;
    [Range(0f, 1f)] [SerializeField] float volumeShootSFX = 0.5f;
    [Range(0f, 1f)] [SerializeField] float volumeDeathSFX = 0.75f;

    float shotCounter;

    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, volumeShootSFX);
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
        if (impactVFX == null) { return; }
        Instantiate(impactVFX, transform.position, Quaternion.identity);
    }

    private void Die()
    {
        FindObjectOfType<GameSession>().AddToScore(scorePoints);
        Destroy(gameObject);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, volumeDeathSFX);        
    }

}
