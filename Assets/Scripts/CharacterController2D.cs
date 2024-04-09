using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] Rigidbody2D rb;
    Vector2 movement;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
            movement.y = Input.GetAxisRaw("Vertical");
            movement.x = Input.GetAxisRaw("Horizontal");
        if (movement != Vector2.zero)
        {
        animator.SetFloat("motionY", movement.y);
        animator.SetFloat("motionX", movement.x);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
