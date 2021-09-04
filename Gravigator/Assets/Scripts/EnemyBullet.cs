using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int dmgAmount;

    private void Start()
    {
        Destroy(gameObject, 20f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
            return;

        if (collision.collider.tag == "EnemyProjectile")
            return;

        if (collision.collider.tag == "Player")
            collision.collider.GetComponent<Player>().TakeDamage(dmgAmount);

        GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
