using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D rigidbody2D;
    public Transform gunHand;
    private Camera camera;
    public Animator animator;
    public GameObject bullet;
    public Transform gunBarrel;
    public float timeBetweenShots;
    private float shotCounter;
    

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        camera = Camera.main;
        rigidbody2D.freezeRotation = true;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();



        rigidbody2D.velocity = moveInput * moveSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = camera.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        if (Mathf.Cos(angle * Mathf.Deg2Rad) < 0)
        {
            gunHand.rotation = Quaternion.Euler(180, 0, -angle);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            gunHand.rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        Shot();
        SetAnimation();

    }

    private void SetAnimation()
    {
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, gunBarrel.position, gunBarrel.rotation);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bullet, gunBarrel.position, gunBarrel.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }
}
