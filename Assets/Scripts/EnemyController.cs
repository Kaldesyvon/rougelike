using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rigidbody;
    public Animator animator;
    public float agroRange;
    private Vector3 moveDirection;
    public int health = 100;
    public GameObject[] deathSplatters;
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public SpriteRenderer body;



    void Start()
    {
        rigidbody.freezeRotation = true;
    }

    void Update()
    {
        if(body.isVisible && PlayerController.instance.gameObject.activeInHierarchy){
            if (PlayerController.instance.transform.position.x >= transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= agroRange)
            {
                shouldShoot = true;
                moveDirection = PlayerController.instance.transform.position - transform.position;
                animator.SetBool("isAgroed", true);
            }
            else
            {
                shouldShoot = false;
                moveDirection = Vector3.zero;
                animator.SetBool("isAgroed", false);
            }
            moveDirection.Normalize();
            rigidbody.velocity = moveDirection * moveSpeed;

            if (shouldShoot)
            {
                Shoot();
            }
        }
        else
        {
            shouldShoot = false;
            rigidbody.velocity = Vector2.zero;
            animator.SetBool("isAgroed", false);
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(deathSplatters[Random.Range(0, deathSplatters.Length)], transform.position, Quaternion.Euler(0, 0, 90f * (Random.Range(0, 4))));
        }
    }

    private void Shoot()
    {
        fireCounter -= Time.deltaTime;
        if (fireCounter <= 0)
        {
            fireCounter = fireRate;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
}
