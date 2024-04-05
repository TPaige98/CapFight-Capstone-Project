using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float jabRange = 1f;
    public float punchRange = 3f;
    public float jumpHeight = 20f;
    public float walkSpeed = 3f;
    public float jumpChance = 0.5f;

    float enemyDirection;
    int jumpsLeft = 1;

    public Transform Player;
    public Timer timerScript;
    public PlayerMovement playerMoves;

    public LayerMask groundLayer;

    public bool isFlipped = false;

    public float blockChance = 0.0005f;
    private bool isBlocking = false;

    public float blockLength = 3.0f;
    private float blockCooldown = 0f;

    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(1.45f, 0.05f);
    private bool shouldJump;

    private Animator animator;
    Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = animator.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMoves = Player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        Idle();
        LookAtPlayer();
        Move();
        Jab();
        Punch();
        Block();

        Debug.Log("Is blocking: " + isBlocking);
        Debug.Log("Is Grounded: " + isGrounded());

        if (isAttacking)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("EnemyPunch") && !stateInfo.IsName("EnemyJab"))
            {
                isAttacking = false;
            }
        }
    }

    void FixedUpdate()
    {
        Jump();   
    }

    // MOVEMENT -------------------------------------------------------------------------- MOVEMENT//
    public void Move()
    {
        if (timerScript.countdown <= 0 && !isAttacking && !isBlocking && isGrounded())
        {
            enemyDirection = Mathf.Sign(Player.position.x - rb.position.x);
            rb.velocity = new Vector2(enemyDirection * walkSpeed, rb.velocity.y);

            animator.SetFloat("Speed", Mathf.Abs(enemyDirection * walkSpeed));
        }
    }

    public void Idle()
    {
        if (timerScript.countdown > 0)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
    }

    public void Jump()
    {
        if (jumpsLeft > 0)
        {
            if (timerScript.countdown <= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                animator.SetBool("isJumping", true);
                jumpsLeft--;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", false);
            jumpsLeft = 1;
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
    }

    //DEFENSE --------------------------------------------------------------------- DEFENSE//
    public void Block()
    {
        if (timerScript.countdown <= 0 && !isAttacking && blockLength > 0 && isGrounded())
        {
            if (Vector2.Distance(Player.position, rb.position) <= punchRange && playerMoves.isAttacking())
            {
                isBlocking = true;
                animator.SetBool("isBlocking", true);
                rb.velocity = Vector2.zero;
                blockLength -= Time.deltaTime;
            }
        }

        if (blockLength <= 0)
        {
            isBlocking = false;
            animator.SetBool("isBlocking", false);
            blockLength = 3.0f;
        }
    }

    //ATTACKS ------------------------------------------------------------------- ATTACKS//

    private bool isAttacking = false;

    public void Punch()
    {
        if (Vector2.Distance(Player.position, rb.position) <= punchRange && !isAttacking && isGrounded())
        {
            isAttacking = true;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Punch");
        }
    }

    public void Jab()
    {
        if (Vector2.Distance(Player.position, rb.position) <= jabRange && !isAttacking && isGrounded())
        {
            isAttacking = true;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Jab");
        }
    }


    //ORIENTATION ------------------------------------------------------------------- ORIENTATION//

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.x *= -1f;

        if (transform.position.x > Player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            isFlipped = false;
        }
        else if (transform.position.x < Player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            isFlipped = true;
        }
    }

    //PHYSICS -------------------------------------------------------------------------- PHYSICS//

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
