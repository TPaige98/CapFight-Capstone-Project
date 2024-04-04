using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float jabRange = 2f;
    public float punchRange = 4f;
    public float jumpHeight = 12f;
    public float walkSpeed = 3f;
    public float jumpChance = 0.5f;

    public Transform Player;
    public Timer timerScript;

    public LayerMask groundLayer;

    public bool isFlipped = false;

    public float blockChance = 0.5f;
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
    }

    void Update()
    {
        Idle();
        LookAtPlayer();
        //Move();
        Jab();
        Punch();

        if (timerScript.countdown <= 0 && !isAttacking && Random.value < blockChance)
        {
            isBlocking = true;
            animator.SetBool("isBlocking", true);
        }

        if (isBlocking)
        {
            rb.velocity = Vector2.zero;
            blockCooldown -= Time.deltaTime;

            if(blockCooldown <= 0)
            {
                isBlocking = false;
                animator.SetBool("isBlocking", false);
                blockCooldown = blockLength;
            }
            else
            {
                Move();
            }
        }

        if (isAttacking)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("EnemyPunch") && !stateInfo.IsName("EnemyJab"))
            {
                isAttacking = false;
            }
        }

        if (Random.value <= jumpChance && isGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isGrounded() && shouldJump)
        {
            shouldJump = false;
            rb.AddForce(Vector2.up * jumpHeight * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    public void Move()
    {
        if (timerScript.countdown <= 0 && !isAttacking)
        {
            Vector2 target = new Vector2(Player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, walkSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            animator.SetFloat("Speed", walkSpeed);
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
        if (isGrounded())
        {
            rb.AddForce(Vector2.up * jumpHeight * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer);
    }


    //ATTACKS ------------------------------------------------------------------- ATTACKS//

    private bool isAttacking = false;

    public void Punch()
    {
        if (Vector2.Distance(Player.position, rb.position) <= punchRange && !isAttacking)
        {
            isAttacking = true;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Punch");
        }
    }

    public void Jab()
    {
        if (Vector2.Distance(Player.position, rb.position) <= jabRange && !isAttacking)
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
}
