using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float jabRange = 3f;
    public float punchRange = 8f;
    public float runSpeed = 4f;
    public float jumpHeight = 12f;

    public float walkSpeed = 3f;

    public Transform Player;
    public Timer timerScript;

    public LayerMask groundLayer;

    public bool isFlipped = false;
    public float jumpChance = 0.00000001f;

    public float blockDuration = 1.0f;
    private float blockCooldownTimer = 0f;

    private bool isGrounded;
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

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        float direction = Mathf.Sign(Player.position.x - transform.position.x);
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, 1 << Player.gameObject.layer);

        Debug.Log("isGrounded: " + isGrounded);
        Debug.Log("isPlayerAbove: " + isPlayerAbove);

        if (isGrounded)
        {
            rb.velocity = new Vector2(direction * walkSpeed, rb.velocity.y);
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, groundLayer);

            if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }
        }

        //if(timerScript.countdown <= 0 && Random.value < jumpChance)
        //{
        //    animator.SetTrigger("Jump");
        //}
        //else
        //{
        //    Move();
        //}
    }

    public void Move()
    {
        if (timerScript.countdown <= 0)
        {
            Vector2 target = new Vector2(Player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, runSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
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



    private void FixedUpdate()
    {
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (Player.position = transform.position).normalized;

            Vector2 jumpDirection = direction * jumpHeight;

            rb.AddForce(new Vector2(jumpDirection.x, jumpHeight), ForceMode2D.Impulse);
        }
    }



    public void Punch()
    {
        if (Vector2.Distance(Player.position, rb.position) <= punchRange)
        {
            animator.SetTrigger("Punch");
        }
    }



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
