using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Player variables")]
    public int health = 20;
    public float moveSpeed = 5f;

    public int playerScore;

    [Header("HUD Variables")]
    public Text healthTxt;
    public Text scoreTxt;
    private string healthSpacer = "Health:";
    private string scoreSpacer = "Score:";

    [Header("shooting Variables")]
    public float fireRate = 1.5f;
    public float bulletForce = 20f;
    public int bulletDamage = 10;

    private float timeToNextShot;

    [Header("Player Dependencies")]
    public Rigidbody2D rb;
    public Camera cam;
    public GameObject thruster;

    [Header("Shooting Dependencies")]
    public GameObject bulletPrefab;
    public Transform barrelPos;
    public Transform SecondBarrelPos;
    public GameObject muzzleFlashPrimary;
    public GameObject muzzleFlashSecond;
    private bool isPrimaryBarrel;

    [Header("Sound Dependencies")]
    public AudioSource playerSound;
    public AudioClip[] audioEffects;

    [Header("UI Dependencies")]
    public GameObject gameoverScreen;
    public Text gameoverScore;
    public GameObject hudScreen;
    public Texture2D cursorTexture;
    public Vector2 cursorOffset;

    Vector2 movement;
    Vector2 mousePos;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.Auto);
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetButton("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            IsThrusterOn(false);
        }

        UpdateHud();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire2"))
        {
            IsThrusterOn(true);
            Move();
        }
        PlayerRotate();
    }

    void Shoot()
    {
        if (Time.time < timeToNextShot)
            return;

        PlaySound("Shoot");
        timeToNextShot = fireRate + Time.time;
        Transform bp = CurrentActiveBarrel();
        GameObject bullet = Instantiate(bulletPrefab, bp.position, bp.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(bp.up * bulletForce, ForceMode2D.Impulse);
        bullet.GetComponent<Bullet>().dmgAmount = bulletDamage;
        MuzzleFlashOn();
    }

    Transform CurrentActiveBarrel()
    {
        if (isPrimaryBarrel)
        {
            isPrimaryBarrel = !isPrimaryBarrel;
            return (barrelPos);
        }
        else
        {
            isPrimaryBarrel = !isPrimaryBarrel;
            return (SecondBarrelPos);
        }
    }

    void MuzzleFlashOn()
    {
        if (!isPrimaryBarrel)
        {
            muzzleFlashPrimary.SetActive(true);
            Invoke("MuzzleFlashOff", 0.1f);
        }
        else
        {
            muzzleFlashSecond.SetActive(true);
            Invoke("MuzzleFlashOff", 0.1f);
        }
    }

    void MuzzleFlashOff()
    {
        muzzleFlashPrimary.SetActive(false);
        muzzleFlashSecond.SetActive(false);
    }

    void PlayerRotate()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = angle;
    }

    void IsThrusterOn(bool b)
    {
        if(b == true)
            thruster.SetActive(true);
        else
            thruster.SetActive(false);
    }
    void Move()
    {
        rb.AddForce(this.transform.up * moveSpeed, ForceMode2D.Force);
    }

    void UpdateHud()
    {
        string healthText = "";
        for (int i = 0; i < health; i++)
        {
            healthText = healthText + "|";
        }

        healthTxt.text = healthSpacer + healthText;

        scoreTxt.text = scoreSpacer + playerScore;
    }

    public void TakeDamage(int dmg)
    {
    
        health -= dmg;
        PlaySound("Damage");
        if (health <= 0)
            Death();
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }

    void Death()
    {
        gameoverScore.text = scoreSpacer + playerScore;
        gameoverScreen.SetActive(true);
        hudScreen.SetActive(false);
        Time.timeScale = 0f;
        Debug.Log("Player is Dead!");
    }


    public void PlaySound(string id)
    {
        switch (id)
        {
            case "Shoot":
                playerSound.clip = audioEffects[0];
                playerSound.Play();
                break;
            case "Damage":
                playerSound.clip = audioEffects[1];
                playerSound.Play();
                break;
            case "Powerup":
                playerSound.clip = audioEffects[2];
                playerSound.Play();
                break;
            default:
                Debug.Log("UwU whats this? I cant find " + id);
                return;
        }
    }
}
