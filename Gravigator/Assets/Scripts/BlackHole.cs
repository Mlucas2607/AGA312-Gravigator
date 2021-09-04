using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public GameObject parent;
    public AudioSource blackholeAudio;
    public float growthRate = 0.01f;
    public float blackHoleSize = 0.3f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
            Destroy(collision.gameObject);
        if (collision.collider.tag == "Player")
            collision.collider.GetComponent<Player>().TakeDamage(1000);

        blackholeAudio.Play();
        blackHoleSize += growthRate;
        SetBlackHoleSize();
    }

    void SetBlackHoleSize()
    {
        Vector3 scale;
        scale.x = blackHoleSize;
        scale.y = blackHoleSize;
        scale.z = blackHoleSize;

        parent.transform.localScale = scale;
    }
}
