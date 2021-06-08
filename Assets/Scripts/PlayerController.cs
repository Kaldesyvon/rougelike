using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D rigidbody2D;
    public Transform gunHand;
    private Camera camera;
    public Animator animator;

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

        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        rigidbody2D.velocity = moveInput * moveSpeed;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = camera.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        // hand.rotation = Quaternion.Euler(0, 0, angle);
        if (Mathf.Cos(angle * Mathf.Deg2Rad) < 0)
        {
            // gunHand.transform.localScale *= -1; 
            // gunHand.transform.Rotate(new Vector3(180,0,0));
            gunHand.rotation = Quaternion.Euler(180, 0, -angle);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            gunHand.rotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }
}
