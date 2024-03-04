using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Variables for Character Movement//
    float horizontalInput;
    float moveSpeed = 5f;


    float jumpHeight = 9.25f;
    int maxJumps = 1;


    bool isGrounded = false;
    //bool isFacingLeft = false;

    public Rigidbody2D rb;

    Animator animator;


    //Audio Sources
    [SerializeField] private AudioSource JabSoundEffect;
    [SerializeField] private AudioSource PunchSoundEffect;

    //Start
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //Updates
    void Update()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        animator.SetFloat("HorizontalMovement", Math.Abs(rb.velocity.x));
        animator.SetFloat("VerticalMovement", rb.velocity.y);
    }

    //Movement
    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
    }
    
    //Jumping
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded && maxJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            animator.SetBool("isJumping", isGrounded);
            --maxJumps;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
        maxJumps = 1;
    }

    //Attacks
    public void Jab(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded)
            {
                //JabSoundEffect.Play();
                animator.SetTrigger("Jab");
            }
        }
    }

    public void Punch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded)
            {
                //PunchSoundEffect.Play();
                animator.SetTrigger("Punch");
            }
        }    
    }
}
