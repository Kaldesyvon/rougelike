using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 8f;
    public Rigidbody2D rigidbody2D;
    public GameObject impactEffect;
    public GameObject enemyHit;

    void Start()
    {

    }

    void Update()
    {
        rigidbody2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(60);
            Instantiate(enemyHit, transform.position, transform.rotation);
        }
        else Instantiate(impactEffect, transform.position, transform.rotation); 

        Destroy(gameObject);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
