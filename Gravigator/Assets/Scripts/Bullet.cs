using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public int dmgAmount;

    private void Start()
    {
        Destroy(gameObject, 20f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
            return;

        if (collision.collider.tag == "Projectile")
            return;

        if (collision.collider.tag == "Enemy")
            collision.collider.GetComponent<Enemy>().TakeDamage(dmgAmount);
 

        GameObject effect = Instantiate(hitEffect, transform.position,transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
