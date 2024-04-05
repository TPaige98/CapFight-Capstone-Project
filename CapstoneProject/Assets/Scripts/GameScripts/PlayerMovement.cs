using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Variables for Character Movement
    public float horizontalInput;
    public float walkSpeed;

    //Varibles for Character Jumping
    public float jumpHeight;
    int maxJumps = 2;
    int jumpsLeft;

    //Variables for Checking Ground
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(1.45f, 0.05f);
    public LayerMask groundLayer;
    public LayerMask oneWayLayer;

    //Variables for Gravity
    public float initialGravity = 2f;
    public float maxGravity = 20f;
    public float fallSpeedGravity = 2f;

    //Variables for Blocking and Direction
    bool isBlocking;
    bool isCrouching;
    bool isFacingRight = true;

    //Variables for Unity Objects
    public Rigidbody2D rb;
    public Transform opponentTransform;
    public Timer timerScript;
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
        if (!PauseMenu.isPaused && !RestartMenu.isPaused)
        {
            if (timerScript.countdown > 0)
            {
                horizontalInput = 0f;
                rb.velocity = Vector2.zero;
            }
            else
            {
                if (isGrounded())
                {
                    rb.velocity = new Vector2(horizontalInput * walkSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

                }
            }

            animator.SetBool("isCrouching", isCrouching);
            animator.SetBool("isBlocking", isBlocking);
            animator.SetFloat("HorizontalMovement", Math.Abs(rb.velocity.x));
            animator.SetFloat("VerticalMovement", rb.velocity.y);

            FlipCharacter();
            doubleJump();
            gravity();
        }
    }

    //MOVEMENT ------------------------------------------------------------ MOVEMENT//
    public void Move(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
        rb.velocity = new Vector2(horizontalInput * walkSpeed, rb.velocity.y);
    }

    void FlipCharacter()
    {
        if (!isFacingRight && horizontalInput > 0f || isFacingRight && horizontalInput < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //JUMPING --------------------------------------------------------------- JUMPING//
    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsLeft > 0)
        {
            if (context.performed && context.action.name == "Jump" && timerScript.countdown <= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                animator.SetBool("isJumping", true);
                jumpsLeft--;
            }
            else if (context.canceled && context.action.name == "Jump" && timerScript.countdown <= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                animator.SetBool("isJumping", true);
                jumpsLeft--;
            }
        }
        Debug.Log("Is Character Jumping: ");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            rb.AddForce(direction * 8f, ForceMode2D.Impulse);
            rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
    }

    private void doubleJump()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsLeft = maxJumps;
        }
    }

    //ATTACKS ---------------------------------------------------------------------------------- ATTACKS//
    public void Jab(InputAction.CallbackContext context)
    {
        if (context.performed && timerScript.countdown <= 0 && isGrounded() && !RestartMenu.isPaused && !PauseMenu.isPaused)
        {
            animator.SetTrigger("Jab");
            JabSoundEffect.Play();
        }
    }

    public void Punch(InputAction.CallbackContext context)
    {
        if (context.performed && timerScript.countdown <= 0 && isGrounded() && !RestartMenu.isPaused && !PauseMenu.isPaused)
        {
            animator.SetTrigger("Punch");
            PunchSoundEffect.Play();
        }
    }

    public bool isAttacking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerJab") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerPunch");
    }

    //DEFENSE ------------------------------------------------------------------------------------ DEFENSE//
    public void Block(InputAction.CallbackContext context)
    {
        if (context.performed && timerScript.countdown <= 0 && !RestartMenu.isPaused && !PauseMenu.isPaused)
        {
            isBlocking = true;
        }
        else
        {
            isBlocking = false;
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed && timerScript.countdown <= 0 && !RestartMenu.isPaused && !PauseMenu.isPaused)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
    }

    //PHYSICS ----------------------------------------------------------------------------------------- PHYSICS//
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }

    private void gravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = initialGravity * fallSpeedGravity;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxGravity));
        }
        else
        {
            rb.gravityScale = initialGravity;
        }
    }
}
