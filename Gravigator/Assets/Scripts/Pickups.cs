using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [Header("Pickup Settings")]
    public string pickupType;
    public float fireRateBuff = 0.01f;
    public int healAmount = 20;
    public int damageIncrease = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ApplyEffect(collision.gameObject);
            Destroy(gameObject);
        }
            
    }

    void ApplyEffect(GameObject player)
    {
        player.GetComponent<Player>().PlaySound("Powerup");
        switch (pickupType)
        {
            case "FireRate":
                player.GetComponent<Player>().fireRate -= fireRateBuff;
                break;
            case "Heal":
                player.GetComponent<Player>().health += healAmount;
                break;
            case "Damage":
                player.GetComponent<Player>().bulletDamage += damageIncrease;
                break;
            default:
                return;
        }
    }
}
