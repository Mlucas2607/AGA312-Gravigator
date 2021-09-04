using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Variables")]
    public float speed = 5f;
    public int health = 100;
    public float fireRate = 0.5f;
    public float bulletForce = 20f;
    public int bulletDamage = 10;
    public float bulletSpread = 0.1f;
    public int scoreWorth = 10;

    private float timeToNextShot;

    [Header("Enemy Dependencies")]
    public GameObject[] itemDrops;
    public Transform barrelPos;
    public GameObject bulletPrefab;
    public GameObject player;
    public Rigidbody2D rb;
    public AudioSource enemyAudio;
    public AudioClip[] enemyAudioEffects;

    private Vector2 movement;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Shoot();
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Shoot()
    {
        if (Time.time < timeToNextShot)
            return;
        PlaySound("Shoot");
        timeToNextShot = Time.time + fireRate;
        Quaternion spread = BulletSpread();
        GameObject bullet = Instantiate(bulletPrefab, barrelPos.position, spread);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.forward * bulletForce, ForceMode2D.Impulse);
        bullet.GetComponent<EnemyBullet>().dmgAmount = bulletDamage;
    }

    void Move()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 lookDir = playerPos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        lookDir.Normalize();
        movement = lookDir;
        rb.MovePosition((Vector2)transform.position + (movement * speed * Time.deltaTime));
    }

    Quaternion BulletSpread()
    {
        Quaternion rot = new Quaternion(0,0,0,0);
        rot.y = transform.rotation.y + Random.Range(-bulletSpread,bulletSpread);
        return (rot);
    }
    public void TakeDamage(int dmg)
    {
        if (health <= 0)
            Death();
        PlaySound("Damage");
        health -= dmg;
    }

    private void Death()
    {
        GameObject pickup = itemDrops[Random.Range(0, itemDrops.Length)];
        Instantiate(pickup, transform.position, Quaternion.identity);
        player.GetComponent<Player>().AddScore(scoreWorth);
        Destroy(gameObject);
    }

    public void PlaySound(string id)
    {
        switch (id)
        {
            case "Shoot":
                enemyAudio.clip = enemyAudioEffects[0];
                enemyAudio.Play();
                break;
            case "Damage":
                enemyAudio.clip = enemyAudioEffects[1];
                enemyAudio.Play();
                break;
            default:
                Debug.Log("UwU whats this? I cant find " + id);
                return;
        }
    }
}
